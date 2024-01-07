using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 128;

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static Email From(string email)
    {
        return new Email(email);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}