using EShop.Application.Persistance.ESopSchema.ProductRepositories;
using EShop.Infrastructure.Persistance.DbContexts;

namespace EShop.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductReadRepository : GenericPrimitiveReadRepositoryBase<EshopReadDbContext>, IProductReadRepository
{
    public ProductReadRepository(EshopReadDbContext dbContext) : base(dbContext) { }
}

