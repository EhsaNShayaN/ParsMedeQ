namespace ParsMedeq.Domain.Events;

public interface IEventProvider
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void AddDomainEvent(IDomainEvent @event);
    void AddDomainEvents(IEnumerable<IDomainEvent> events);
    void Clear();
}
