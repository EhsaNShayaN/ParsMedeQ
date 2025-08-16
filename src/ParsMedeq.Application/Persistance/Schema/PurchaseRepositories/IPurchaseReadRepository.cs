using ParsMedeQ.Domain.Aggregates.PurchaseAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
public interface IPurchaseReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Purchase>> GetPurchase(
        int TableId,
        int RelatedId,
        int UserId,
        CancellationToken cancellationToken);
}
