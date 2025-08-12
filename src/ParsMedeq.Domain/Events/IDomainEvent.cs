namespace ParsMedeq.Domain.Events;

public interface IDomainEvent
{
    bool IsIntegrationEvent { get; }
    DateTimeOffset OccuredOn { get; }
}
