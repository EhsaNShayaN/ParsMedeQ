using ParsMedeq.Domain.Aggregates.ProductAggregate;
using ParsMedeq.Domain.Persistance;

namespace ParsMedeq.Application.Persistance.ESopSchema.ProductRepositories;
public interface IProductWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Product>> AddNewProduct(Product product);
}
