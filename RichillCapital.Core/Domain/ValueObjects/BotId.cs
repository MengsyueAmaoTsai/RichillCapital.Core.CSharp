using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class BotId : ValueObject
{
    public const int MaxLength = 128;

    public BotId(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}