using ParsMedeQ.Domain.Events;

namespace ParsMedeQ.Domain.Abstractions;

public interface IEventEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
