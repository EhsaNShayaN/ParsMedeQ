namespace ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;

public sealed class PaymentListDbQueryResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte Status { get; set; }
    public string MediaPath { get; set; } = string.Empty;
    public int Code { get; set; }
    public DateTime CreationDate { get; set; }
    public PaymentAnswerDbQueryResponse[] Answers { get; set; } = [];
}
public sealed class PaymentAnswerDbQueryResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string MediaPath { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
}