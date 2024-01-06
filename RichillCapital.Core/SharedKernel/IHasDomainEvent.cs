namespace RichillCapital.Core.SharedKernel;

public interface IHasDomainEvent
{
    void ClearDomainEvents();

    IEnumerable<DomainEvent> GetDomainEvents();

    void RegisterDomainEvent(DomainEvent domainEvent);
}