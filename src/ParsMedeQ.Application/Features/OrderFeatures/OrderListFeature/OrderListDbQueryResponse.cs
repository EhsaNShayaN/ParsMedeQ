namespace ParsMedeQ.Application.Features.OrderFeatures.OrderListFeature;

public sealed class OrderListDbQueryResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string OrderNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal? FinalAmount { get; set; }
    public byte Status { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime CreationDate { get; set; }
}