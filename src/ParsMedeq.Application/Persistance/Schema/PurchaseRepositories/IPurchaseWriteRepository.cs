using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
public interface IPurchaseWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Purchase>> AddPurchase(Purchase Purchase, CancellationToken cancellationToken);
}
