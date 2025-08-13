namespace ParsMedeQ.Domain.Events;
public abstract record DomainEventBase(DateTimeOffset OccuredOn) : IDomainEvent
{
    public bool IsIntegrationEvent => false;
}
