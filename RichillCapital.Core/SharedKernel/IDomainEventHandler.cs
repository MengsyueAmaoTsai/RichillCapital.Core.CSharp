using MediatR;

namespace RichillCapital.Core.SharedKernel;

public interface IDomainEventHandler<TDomainEvent> :
    INotificationHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
    new Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
}