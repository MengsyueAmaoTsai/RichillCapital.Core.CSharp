using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class UserId : ValueObject
{
    public const int MaxLength = 128;

    public UserId(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}