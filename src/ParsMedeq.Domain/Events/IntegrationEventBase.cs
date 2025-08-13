using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Domain.Events;

public abstract record IntegrationEventBase(DateTimeOffset OccuredOn) : IDomainEvent
{
    public bool IsIntegrationEvent => true;
}
public abstract record IntegrationEventBase<TId>(TId Id)
    : IntegrationEventBase(DateHelpers.Now)
    where TId : IEquatable<TId>
{ }