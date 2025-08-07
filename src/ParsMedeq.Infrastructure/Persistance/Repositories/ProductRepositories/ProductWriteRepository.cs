using EShop.Application.Persistance.ESopSchema.ProductRepositories;
using EShop.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace EShop.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductWriteRepository : GenericPrimitiveWriteRepositoryBase<EshopWriteDbContext>, IProductWriteRepository
{
    public ProductWriteRepository(EshopWriteDbContext dbContext) : base(dbContext){ }

    public ValueTask<PrimitiveResult<Product>> AddNewProduct(Product product) => this.Add(product);
}
