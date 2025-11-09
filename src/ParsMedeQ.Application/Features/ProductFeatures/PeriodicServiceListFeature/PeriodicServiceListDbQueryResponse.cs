using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.UserAggregate;

namespace ParsMedeQ.Application.Features.ProductFeatures.PeriodicServiceListFeature;

public sealed class PeriodicServiceListDbQueryResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public DateTime ServiceDate { get; set; }
    public bool Done { get; set; }
    public DateTime CreationDate { get; set; }
}