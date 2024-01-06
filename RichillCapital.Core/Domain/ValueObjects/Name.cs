using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class Name : ValueObject
{
    public const int MaxLength = 128;

    public Name(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}