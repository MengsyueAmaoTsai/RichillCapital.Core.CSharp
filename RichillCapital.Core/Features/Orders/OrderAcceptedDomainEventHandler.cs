using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Orders;

internal sealed class OrderAcceptedDomainEventHandler :
    IDomainEventHandler<OrderAcceptedDomainEvent>
{
    private readonly ILogger<OrderAcceptedDomainEventHandler> _logger;
    private readonly IRepository<Order> _orderRepository;

    public OrderAcceptedDomainEventHandler(
        ILogger<OrderAcceptedDomainEventHandler> logger,
        IRepository<Order> orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderAcceptedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("On {event}", domainEvent.GetType().Name);

        var orderMaybe = await _orderRepository
            .FirstOrDefaultAsync(order => order.Id == domainEvent.OrderId, cancellationToken);

        if (!orderMaybe.HasValue)
        {
            throw new InvalidOperationException();
        }
    }
}