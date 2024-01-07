using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.Errors;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.Create;

internal sealed class CreateBotCommandHandler : ICommandHandler<CreateBotCommand, ErrorOr<BotId>>
{
    private readonly IRepository<Bot> _botRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBotCommandHandler(IRepository<Bot> botRepository, IUnitOfWork unitOfWork)
    {
        _botRepository = botRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<BotId>> Handle(CreateBotCommand command, CancellationToken cancellationToken)
    {
        var botId = new BotId(command.BotId);
        var name = new Name(command.Name);
        var platformResult = TradingPlatform.FromName(command.TradingPlatform, true);

        if (!platformResult.IsSuccess)
        {
            return platformResult.Error;
        }

        if (await _botRepository.AnyAsync(
            bot => bot.Id == botId,
            cancellationToken))
        {
            return BotErrors.Duplicated(botId);
        }

        if (await _botRepository.AnyAsync(
            bot => bot.Name == name,
            cancellationToken))
        {
            return BotErrors.Duplicated(name);
        }

        var bot = Bot.Create(
            botId,
            name,
            new Description(command.Description),
            platformResult.Value);

        _botRepository.Add(bot);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bot.Id;
    }
}