using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Signals.Emit;

public sealed record EmitSignalCommand(
    DateTimeOffset Time,
    string Symbol,
    string TradeType,
    decimal Quantity,
    decimal Price) : ICommand<Result>;