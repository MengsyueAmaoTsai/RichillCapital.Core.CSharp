using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class OrderId : ValueObject
{
    public const int MaxLength = 128;

    private OrderId(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static OrderId From(string id)
    {
        return new OrderId(id);
    }

    public static OrderId New()
    {
        return new OrderId("OID-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}