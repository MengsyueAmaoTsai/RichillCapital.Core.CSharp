using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Orders;

internal sealed class OrderCreatedDomainEventHandler :
    IDomainEventHandler<OrderCreatedDomainEvent>
{
    private readonly ILogger<OrderCreatedDomainEventHandler> _logger;

    public OrderCreatedDomainEventHandler(
        ILogger<OrderCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("On {event}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}