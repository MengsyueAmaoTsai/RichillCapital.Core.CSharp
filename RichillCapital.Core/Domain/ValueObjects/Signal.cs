using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class Signal : ValueObject
{
    private Signal(
        DateTimeOffset time,
        Symbol symbol,
        TradeType tradeType,
        decimal quantity,
        decimal price)
    {
        Time = time;
        Symbol = symbol;
        TradeType = tradeType;
        Quantity = quantity;
        Price = price;
    }

    public DateTimeOffset Time { get; private init; }

    public Symbol Symbol { get; private init; }

    public TradeType TradeType { get; private init; }

    public decimal Quantity { get; private init; }

    public decimal Price { get; private init; }

    public static Signal Emit(
        DateTimeOffset time,
        Symbol symbol,
        TradeType tradeType,
        decimal quantity,
        decimal price)
    {
        var signal = new Signal(
            time,
            symbol,
            tradeType,
            quantity,
            price);

        return signal;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Time;
        yield return Symbol;
        yield return TradeType;
        yield return Quantity;
        yield return Price;
    }
}