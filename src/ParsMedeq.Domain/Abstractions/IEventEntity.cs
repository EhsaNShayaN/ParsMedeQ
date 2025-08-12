using ParsMedeq.Domain.Events;

namespace ParsMedeq.Domain.Abstractions;

public interface IEventEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
