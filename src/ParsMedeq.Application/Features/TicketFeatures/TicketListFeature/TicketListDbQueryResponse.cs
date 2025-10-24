namespace ParsMedeQ.Application.Features.TicketFeatures.TicketListFeature;

public sealed class TicketListDbQueryResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte Status { get; set; }
    public string MediaPath { get; set; } = string.Empty;
    public int Code { get; set; }
    public DateTime CreationDate { get; set; }
    public TicketAnswerDbQueryResponse[] Answers { get; set; } = [];
}
public sealed class TicketAnswerDbQueryResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string MediaPath { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
}