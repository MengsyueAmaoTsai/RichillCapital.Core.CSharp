using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class PositionId : ValueObject
{
    private PositionId(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static PositionId New() =>
         new("PID-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

    public static PositionId From(string id)
    {
        return new PositionId(id);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}