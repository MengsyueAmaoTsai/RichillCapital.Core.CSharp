using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Signals.Emit;

internal sealed class SignalEmittedDomainEventHandler :
    IDomainEventHandler<SignalEmittedDomainEvent>
{
    private readonly ILogger<SignalEmittedDomainEventHandler> _logger;

    public SignalEmittedDomainEventHandler(
        ILogger<SignalEmittedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SignalEmittedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}
