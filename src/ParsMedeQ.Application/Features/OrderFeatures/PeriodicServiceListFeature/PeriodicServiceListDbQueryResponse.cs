using ParsMedeQ.Domain.Aggregates.UserAggregate;

namespace ParsMedeQ.Application.Features.OrderFeatures.PeriodicServiceListFeature;

public sealed class PeriodicServiceListDbQueryResponse
{
    public int Id { get; set; }
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public User User { get; set; } = null!;
    public int RelatedId { get; set; }
    public DateTime ServiceDate { get; set; }
    public bool Done { get; set; }
    public bool HasNext { get; set; }
    public DateTime? GuarantyExpirationDate { get; set; }
}