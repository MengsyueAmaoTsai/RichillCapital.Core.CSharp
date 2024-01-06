using MediatR;

namespace RichillCapital.Core.SharedKernel;

public sealed class MediatRDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublisher _publisher;

    public MediatRDomainEventDispatcher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task DispatchAndClearDomainEvents(IEnumerable<IHasDomainEvent> entitiesWithDomainEvents)
    {
        foreach (var entity in entitiesWithDomainEvents)
        {
            var events = entity
                .GetDomainEvents()
                .ToArray();

            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent)
                    .ConfigureAwait(false);
            }
        }
    }
}