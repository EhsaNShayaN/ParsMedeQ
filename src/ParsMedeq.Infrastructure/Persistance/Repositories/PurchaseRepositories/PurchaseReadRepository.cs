using Azure.Core;
using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.PurchaseRepositories;
internal sealed class PurchaseReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IPurchaseReadRepository
{
    public PurchaseReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Purchase>> GetPurchase(
        int TableId,
        int RelatedId,
        int UserId,
        CancellationToken cancellationToken)
    {
        return this.DbContext
            .Purchase
            .Where(s => s.TableId == TableId && s.RelatedId == RelatedId && s.UserId == UserId)
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "آیتم مورد نظر موجود نمی باشد."))
            .Map(a => a!);
    }
}