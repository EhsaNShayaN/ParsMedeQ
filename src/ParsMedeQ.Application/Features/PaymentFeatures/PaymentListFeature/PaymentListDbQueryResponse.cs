namespace ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;

public sealed class PaymentListDbQueryResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public byte PaymentMethod { get; set; }
    public string? TransactionId { get; set; }
    public byte Status { get; set; }
    public DateTime? PaidDate { get; set; }
    public DateTime CreationDate { get; set; }
}