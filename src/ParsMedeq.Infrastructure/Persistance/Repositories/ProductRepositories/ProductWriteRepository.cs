using ParsMedeq.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeq.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeq.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IProductWriteRepository
{
    public ProductWriteRepository(WriteDbContext dbContext) : base(dbContext){ }

    public ValueTask<PrimitiveResult<Product>> AddNewProduct(Product product) => this.Add(product);
}
