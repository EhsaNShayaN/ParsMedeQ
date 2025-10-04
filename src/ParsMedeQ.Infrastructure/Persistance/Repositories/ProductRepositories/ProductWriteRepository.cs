using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IProductWriteRepository
{
    public ProductWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Product>> FindById(int id, CancellationToken cancellationToken = default) =>
        this.DbContext
            .Product
            .Include(s => s.ProductTranslations)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "محصولی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult<Product>> FindByIdWithMediaList(int id, CancellationToken cancellationToken = default) =>
        this.DbContext
            .Product
            .Include(s => s.ProductMediaList)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "محصولی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult<Product>> AddProduct(Product Product, CancellationToken cancellationToken) =>
        this.Add(Product);
    public ValueTask<PrimitiveResult<ProductMedia>> AddProductMedia(ProductMedia ProductMedia, CancellationToken cancellationToken) =>
        this.Add(ProductMedia);
    public ValueTask<PrimitiveResult<ProductMedia>> DeleteProductMedia(ProductMedia ProductMedia, CancellationToken cancellationToken) =>
        this.Remove(ProductMedia);
    public ValueTask<PrimitiveResult<Product>> UpdateProduct(Product Product, CancellationToken cancellationToken) =>
        this.Update(Product);
    public ValueTask<PrimitiveResult<ProductCategory>> FindCategoryById(int id, CancellationToken cancellationToken) =>
        this.DbContext
            .ProductCategory
            .Include(s => s.ProductCategoryTranslations)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "دسته بندی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult<ProductCategory>> AddProductCategory(ProductCategory ProductCategory, CancellationToken cancellationToken) =>
        this.Add(ProductCategory);
    public ValueTask<PrimitiveResult<ProductCategory>> UpdateProductCategory(ProductCategory ProductCategory, CancellationToken cancellationToken) =>
        this.Update(ProductCategory);
}
