using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
public interface IProductWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Product>> FindById(int id, CancellationToken cancellationToken = default);
    ValueTask<PrimitiveResult<Product[]>> FindByIds(int[] id, CancellationToken cancellationToken = default);
    ValueTask<PrimitiveResult<Product>> FindByIdWithMediaList(int id, CancellationToken cancellationToken = default);
    ValueTask<PrimitiveResult<Product>> FindByPeriodicService(int id, CancellationToken cancellationToken = default);
    ValueTask<PrimitiveResult<Product>> AddProduct(Product Product, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductMedia>> AddProductMedia(ProductMedia ProductMedia, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductMedia>> DeleteProductMedia(ProductMedia ProductMedia, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Product>> UpdateProduct(Product Product, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductCategory>> FindCategoryById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductCategory>> FindCategoryWithProducts(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductCategory>> AddProductCategory(ProductCategory ProductCategory, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductCategory>> UpdateProductCategory(ProductCategory ProductCategory, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Product>> Delete(Product product, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProductCategory>> DeleteCategory(ProductCategory productCategory, CancellationToken cancellationToken);
}
