using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.Enumerations;

public sealed class TradeType : Enumeration<TradeType>
{
    private static readonly TradeType None = new(nameof(None), 0);
    private static readonly TradeType Buy = new(nameof(Buy), 1);
    private static readonly TradeType Sell = new(nameof(Sell), -1);

    private TradeType(string name, int value)
        : base(name, value)
    {
    }
}