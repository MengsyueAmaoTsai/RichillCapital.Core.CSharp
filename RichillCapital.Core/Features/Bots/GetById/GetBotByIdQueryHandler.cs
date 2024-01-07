using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.GetById;

internal sealed class GetBotByIdQueryHandler : IQueryHandler<GetBotByIdQuery, Result<BotDto>>
{
    private readonly IReadonlyRepository<Bot> _botRepository;

    public GetBotByIdQueryHandler(IReadonlyRepository<Bot> botRepository) =>
        _botRepository = botRepository;

    public async Task<Result<BotDto>> Handle(GetBotByIdQuery query, CancellationToken cancellationToken)
    {
        var botMaybe = await _botRepository.GetByIdAsync(new BotId(query.BotId), cancellationToken);

        return !botMaybe.HasValue ?
            Error.NotFound("Bot is not found.") :
            botMaybe
                .AsResult()
                .Map(bot => new BotDto(
                    bot.Id.Value,
                    bot.Name.Value,
                    bot.Description.Value,
                    bot.Platform.Name));
    }
}