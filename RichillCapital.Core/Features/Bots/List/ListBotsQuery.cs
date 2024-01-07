using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.List;

public sealed record ListBotsQuery() :
    IQuery<Result<IEnumerable<BotDto>>>;