using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class UserId : ValueObject
{
    public const int MaxLength = 128;

    private UserId(string value)
    {
        Value = value;
    }

    public string Value { get; private init; }

    public static UserId From(string id)
    {
        return new UserId(id);
    }

    public static UserId New() => new("UID" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}