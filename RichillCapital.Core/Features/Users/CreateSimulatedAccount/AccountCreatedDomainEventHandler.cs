using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Users.CreateSimulatedAccount;

internal sealed class AccountCreatedDomainEventHandler : IDomainEventHandler<AccountCreatedDomainEvent>
{
    private readonly ILogger<AccountCreatedDomainEventHandler> _logger;

    public AccountCreatedDomainEventHandler(ILogger<AccountCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AccountCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("On {event}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}