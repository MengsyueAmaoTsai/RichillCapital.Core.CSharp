using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class AccountId : ValueObject
{
    public const int MaxLength = 128;

    public const string SimulatedPrefix = "SIM";
    public const string MockPrefix = "MOC";

    private AccountId(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static AccountId NewSimulatedId() => NewWithPrefix(SimulatedPrefix);

    public static AccountId NewMockId() => NewWithPrefix(MockPrefix);

    public static AccountId From(string id)
    {
        return new AccountId(id);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    private static AccountId NewWithPrefix(string prefix) =>
        new(string.Join('-', prefix, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
}