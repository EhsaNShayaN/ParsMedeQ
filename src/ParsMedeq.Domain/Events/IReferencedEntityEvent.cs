using EShop.Domain.Abstractions;
using EShop.Domain.Types;

namespace EShop.Domain.Events;

public interface IReferencedEntityEvent<TEntity, TId, TIdValue, TEvent>
    where TEntity : EntityBase<TId>
    where TId : IEquatable<TId>, IDbType<TIdValue>
    where TEvent : IDomainEvent
{
    TEntity SourceObject { get; }
}
