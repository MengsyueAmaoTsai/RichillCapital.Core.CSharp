using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.GetById;

public sealed record GetBotByIdQuery(string BotId) : IQuery<Result<BotDto>>;