using ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductDetailsFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductListFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductMediaListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
public interface IProductReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Product>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<BasePaginatedApiResponse<ProductListDbQueryResponse>>> FilterProducts(
        BasePaginatedQuery paginated,
        string langCode,
        int? productCategoryId,
        int lastId,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductCategoryListDbQueryResponse[]>> FilterProductCategories(
        string langCode,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductDetailsDbQueryResponse>> ProductDetails(
        string langCode,
        int UserId,
        int ProductId,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductCategoryListDbQueryResponse>> ProductCategoryDetails(
        string langCode,
        int id,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductMediaListDbQueryResponse[]>> GetProductMediaList(
        int productId,
        CancellationToken cancellationToken);
}
