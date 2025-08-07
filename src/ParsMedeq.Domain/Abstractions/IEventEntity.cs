using EShop.Domain.Events;

namespace EShop.Domain.Abstractions;

public interface IEventEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
