using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class Money : ValueObject
{
    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; private init; }

    public Currency Currency { get; private init; }

    public static Money Create(decimal amount, Currency currency)
    {
        return new Money(amount, currency);
    }

    public static Money Zero(Currency currency) => new(decimal.Zero, currency);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}