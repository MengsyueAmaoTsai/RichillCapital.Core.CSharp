using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.Delete;

internal sealed class DeleteBotCommandHandler :
    ICommandHandler<DeleteBotCommand, Result>
{
    private readonly IRepository<Bot> _botRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBotCommandHandler(IRepository<Bot> botRepository, IUnitOfWork unitOfWork)
    {
        _botRepository = botRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteBotCommand command, CancellationToken cancellationToken)
    {
        var botMaybe = await _botRepository.GetByIdAsync(new BotId(command.BotId), cancellationToken);

        if (!botMaybe.HasValue)
        {
            return Error.NotFound("Bot not found.");
        }

        _botRepository.Remove(botMaybe.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}