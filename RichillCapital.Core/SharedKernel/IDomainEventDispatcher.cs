namespace RichillCapital.Core.SharedKernel;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearDomainEvents(IEnumerable<IHasDomainEvent> entitiesWithDomainEvents);
}