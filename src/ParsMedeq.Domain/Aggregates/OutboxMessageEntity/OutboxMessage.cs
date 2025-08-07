namespace EShop.Domain.Aggregates.OutboxMessageEntity;
public class OutboxMessage
{
    public long Id { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public DateTimeOffset OccuredOn { get; set; }
    public bool IsIntegrationEvent { get; set; }
    public DateTimeOffset? ProcessedOn { get; set; }
    public string Error { get; set; } = string.Empty;
}
