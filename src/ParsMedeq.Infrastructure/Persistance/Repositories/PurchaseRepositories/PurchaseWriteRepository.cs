using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.PurchaseRepositories;
internal sealed class PurchaseWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IPurchaseWriteRepository
{
    public PurchaseWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Purchase>> AddPurchase(Purchase Purchase, CancellationToken cancellationToken) => this.Add(Purchase);
}
