using EShop.Application.Helpers;
using EShop.Domain.Aggregates.ProductTypeAggregate.Entities;
using EShop.Domain.Persistance;

namespace EShop.Application.Persistance.ESopSchema.ProductRepositories;
public interface IProductReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<BasePaginatedApiResponse<ProductModel>>> FilterProductModels(
        BasePaginatedQuery pageinated,
        CancellationToken cancellationToken);
}
