using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.ESopSchema.ProductRepositories;
public interface IProductWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Product>> AddNewProduct(Product product);
}
