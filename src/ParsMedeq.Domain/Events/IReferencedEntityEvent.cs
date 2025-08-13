using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Types;

namespace ParsMedeQ.Domain.Events;

public interface IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>
    where TEntity : EntityBase<TId>
    where TId : IEquatable<TId>, IDbType<TIdValue>
    where TEvent : IDomainEvent
{
    TEntity SourceObject { get; }
}
