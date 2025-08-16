using ParsMedeQ.Domain.Aggregates.PurchaseAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
public interface IPurchaseWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Purchase>> AddPurchase(Purchase Purchase, CancellationToken cancellationToken);
}
