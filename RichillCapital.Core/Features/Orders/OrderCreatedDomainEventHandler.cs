using Microsoft.Extensions.Logging;

using RichillCapital.Core.Domain.Entities;
using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features.Orders;

internal sealed class OrderCreatedDomainEventHandler :
    IDomainEventHandler<OrderCreatedDomainEvent>
{
    private readonly ILogger<OrderCreatedDomainEventHandler> _logger;
    private readonly IRepository<Order> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCreatedDomainEventHandler(
        ILogger<OrderCreatedDomainEventHandler> logger,
        IRepository<Order> orderRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("On {event}", domainEvent.GetType().Name);

        var orderMaybe = await _orderRepository
            .FirstOrDefaultAsync(order => order.Id == domainEvent.OrderId, cancellationToken);

        if (!orderMaybe.HasValue)
        {
            throw new InvalidOperationException();
        }

        orderMaybe.Value.AsAccepted();

        _orderRepository.Update(orderMaybe.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}