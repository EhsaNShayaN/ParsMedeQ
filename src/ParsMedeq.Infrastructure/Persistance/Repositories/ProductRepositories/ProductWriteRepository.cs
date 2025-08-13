using ParsMedeQ.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IProductWriteRepository
{
    public ProductWriteRepository(WriteDbContext dbContext) : base(dbContext){ }

    public ValueTask<PrimitiveResult<Product>> AddNewProduct(Product product) => this.Add(product);
}
