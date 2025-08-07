using EShop.Domain.Aggregates.ProductAggregate;
using EShop.Domain.Persistance;

namespace EShop.Application.Persistance.ESopSchema.ProductRepositories;
public interface IProductWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Product>> AddNewProduct(Product product);
}
