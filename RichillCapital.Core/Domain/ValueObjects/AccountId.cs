using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class AccountId : ValueObject
{
    public const int MaxLength = 128;

    public AccountId(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static AccountId NewSimulatedId() =>
        new($"SIM-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}