using ParsMedeq.Domain.Abstractions;
using ParsMedeq.Domain.Helpers;
using ParsMedeq.Domain.Types;

namespace ParsMedeq.Domain.Events;

public abstract record ReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>(TEntity SourceObject) :
    IntegrationEventBase(DateHelpers.Now),
    IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>
    where TEntity : EntityBase<TId>
    where TId : IEquatable<TId>, IDbType<TIdValue>
    where TEvent : class, IDomainEvent
{
    public static TEvent CreateEvent(IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent> referencedEvent) =>
        (Activator.CreateInstance(typeof(TEvent), referencedEvent.SourceObject.Id.GetDbValue()) as TEvent)!;
}
