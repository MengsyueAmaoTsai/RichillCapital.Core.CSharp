using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.Enumerations;

public sealed class TradingPlatform : Enumeration<TradingPlatform>
{
    public static readonly TradingPlatform XQ = new(nameof(XQ), 1);
    public static readonly TradingPlatform TradingView = new(nameof(TradingView), 2);
    public static readonly TradingPlatform MetaTrader5 = new("MetaTrader 5", 3);
    public static readonly TradingPlatform CTrader = new(nameof(CTrader), 4);
    public static readonly TradingPlatform MultiCharts = new(nameof(MultiCharts), 5);
    public static readonly TradingPlatform TradeStation = new(nameof(TradeStation), 6);

    private TradingPlatform(string name, int value)
        : base(name, value)
    {
    }
}