using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Entities;

public sealed class Order : Entity<OrderId>
{
    private Order(
        OrderId id,
        AccountId accountId)
        : base(id)
    {
        AccountId = accountId;
    }

    public AccountId AccountId { get; private set; }

    public static Order Create(
        AccountId accountId)
    {
        var order = new Order(
            OrderId.New(),
            accountId);

        order.RegisterDomainEvent(new OrderCreatedDomainEvent(order.Id));

        return order;
    }

    public void AsAccepted()
    {
        RegisterDomainEvent(new OrderAcceptedDomainEvent(Id));
    }
}