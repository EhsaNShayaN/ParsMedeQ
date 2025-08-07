namespace EShop.Domain.Abstractions;

public abstract class AggregateRoot<TId> :
    EntityBase<TId>,
    IAggregateRoot
    where TId : IEquatable<TId>
{
    protected AggregateRoot(TId id) : base(id) { }
}
