using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Users.CreateSimulatedAccount;

internal sealed class AccountCreatedDomainEventHandler : IDomainEventHandler<AccountCreatedDomainEvent>
{
    public AccountCreatedDomainEventHandler()
    {
    }

    public Task Handle(AccountCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}