using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.Create;

public sealed record CreateBotCommand(
    string BotId,
    string Name,
    string Description,
    string TradingPlatform) : ICommand<ErrorOr<BotId>>;