using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Entities;

public sealed class Account : Entity<AccountId>
{
    private readonly List<Balance> _balances = new();

    private Account(
        AccountId id,
        Name name,
        Currency currency)
        : base(id)
    {
        Name = name;
        Currency = currency;
    }

    public Name Name { get; private set; }

    public Currency Currency { get; private set; }

    public IReadOnlyCollection<Balance> Balances => _balances.AsReadOnly();

    public static Account Create(
        AccountId id,
        Name name,
        Currency currency,
        int initialBalance)
    {
        var account = new Account(id, name, currency);

        account.RegisterDomainEvent(new AccountCreatedDomainEvent(account.Id));

        return account;
    }
}