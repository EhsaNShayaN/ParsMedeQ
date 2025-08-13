using ParsMedeQ.Domain.Events;

namespace ParsMedeQ.Domain.Abstractions;
public abstract class EntityBase<TId> :
    IEventEntity
    where TId : IEquatable<TId>
{
    private List<IDomainEvent> _domainEvents = new();

    public TId Id { get; private set; }
    public IReadOnlyCollection<IDomainEvent> Events => _domainEvents.AsReadOnly();

    protected EntityBase(TId id) => Id = id;

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();

    public static IEqualityComparer<EntityBase<TId>> IdEqualityComparer =>
        EqualityComparer<EntityBase<TId>>.Create((x, y) =>
            x is null
                ? y is null
                : y is not null && x.GetType() == y.GetType() && x.Id.Equals(y.Id));
}
