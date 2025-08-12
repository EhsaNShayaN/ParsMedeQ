using ParsMedeq.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeq.Infrastructure.Persistance.DbContexts;

namespace ParsMedeq.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IProductReadRepository
{
    public ProductReadRepository(ReadDbContext dbContext) : base(dbContext) { }
}

