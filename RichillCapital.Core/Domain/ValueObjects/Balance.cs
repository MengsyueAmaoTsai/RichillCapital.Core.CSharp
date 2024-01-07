using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.ValueObjects;

public sealed class Balance : ValueObject
{
    private Balance(
        AccountId accountId, decimal amount, Currency currency)
    {
        AccountId = accountId;
        Amount = amount;
        Currency = currency;
    }

    public AccountId AccountId { get; private init; }

    public decimal Amount { get; private init; }

    public Currency Currency { get; private init; }

    public static Balance Create(AccountId accountId, decimal amount, Currency currency)
    {
        return new Balance(accountId, amount, currency);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
}