namespace RichillCapital.Core.SharedKernel;

public abstract class Entity<TEntityId> : IHasDomainEvent
    where TEntityId : notnull
{
    private readonly List<DomainEvent> _domainEvents = [];

    protected Entity(TEntityId id)
    {
        Id = id;
    }

    public TEntityId Id { get; private set; }

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

    public void RegisterDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}