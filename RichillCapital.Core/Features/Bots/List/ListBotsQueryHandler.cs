using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.List;

internal sealed class ListBotsQueryHandler : IQueryHandler<ListBotsQuery, Result<IEnumerable<BotDto>>>
{
    private readonly IReadonlyRepository<Bot> _botRepository;

    public ListBotsQueryHandler(IReadonlyRepository<Bot> botRepository) => _botRepository = botRepository;

    public async Task<Result<IEnumerable<BotDto>>> Handle(ListBotsQuery query, CancellationToken cancellationToken)
    {
        var bots = await _botRepository
            .ListAsync(cancellationToken);

        return bots
            .Select(bot => new BotDto(
                bot.Id.Value,
                bot.Name.Value,
                bot.Description.Value,
                bot.Platform.Name))
            .ToList()
            .AsReadOnly();
    }
}