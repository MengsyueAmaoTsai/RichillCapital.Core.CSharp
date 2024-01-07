using System.Reflection.Metadata;
using System.Runtime;

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

    public Money Add(decimal amount)
    {
        return new Money(Amount + amount, Currency);
    }

    public Result<Money> Add(Money money)
    {
        if (Currency != money.Currency)
        {
            return Error.Conflict("Currency conflict.");
        }

        return new Money(Amount + money.Amount, Currency);
    }

    public Result<Money> Subtract(decimal amount)
    {
        if (Amount < amount)
        {
            return Error.Conflict("Cannot subtract");
        }

        return new Money(Amount - amount, Currency);
    }

    public Result<Money> Subtract(Money money)
    {
        if (Currency != money.Currency)
        {
            return Error.Conflict("Currency conflict.");
        }

        return Subtract(money.Amount);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}