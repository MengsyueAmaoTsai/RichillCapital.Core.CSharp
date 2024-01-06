using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Users;

internal sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
    private readonly ILogger<UserCreatedDomainEventHandler> _logger;

    public UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("On {event}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}