using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IProductReadRepository
{
    public ProductReadRepository(ReadDbContext dbContext) : base(dbContext) { }
}

