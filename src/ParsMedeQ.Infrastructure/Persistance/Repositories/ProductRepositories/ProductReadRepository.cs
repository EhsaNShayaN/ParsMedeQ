using ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductDetailsFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductListFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductMediaListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Models;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IProductReadRepository
{
    public ProductReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Product>> FindById(int id, CancellationToken cancellationToken) =>
        this.DbContext
            .Product
            .Include(s => s.ProductTranslations)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "محصولی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<ProductListDbQueryResponse>>> FilterProducts(
        BasePaginatedQuery paginated,
        string langCode,
        int? productCategoryId,
        int lastId,
        CancellationToken cancellationToken)
    {
        static BasePaginatedApiResponse<ProductListDbQueryResponse> MapResult(
            PaginateListResult<ProductListDbQueryResponse> paginatedData,
            BasePaginatedQuery pageinated)
        {
            var data = paginatedData.Data.ToArray();
            return new BasePaginatedApiResponse<ProductListDbQueryResponse>(
                data,
                paginatedData.Total,
                pageinated.PageIndex,
                pageinated.PageSize)
            {
                LastId = data.Length > 0 ? data.Last().Id : 0
            };
        }

        var query =
            from res in this.DbContext.Product
            .Include(r => r.ProductTranslations.Where(l => l.LanguageCode == langCode))
            .Include(r => r.ProductCategory)
            .ThenInclude(r => r.ProductCategoryTranslations.Where(l => l.LanguageCode == langCode))
            where res.Deleted == false && ((productCategoryId ?? 0) == 0 || res.ProductCategoryId.Equals(productCategoryId))
            select new ProductListDbQueryResponse
            {
                Id = res.Id,
                Title = res.ProductTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                ProductCategoryId = res.ProductCategoryId,
                ProductCategoryTitle = res.ProductCategory.ProductCategoryTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                Image = res.ProductTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,
                Price = res.Price,
                Discount = res.Discount,
                GuarantyExpirationTime = res.GuarantyExpirationTime,
                PeriodicServiceInterval = res.PeriodicServiceInterval,
                Deleted = res.Deleted,
                Disabled = res.Disabled,
                CreationDate = res.CreationDate,
            };

        if (lastId.Equals(0))
            return query.Paginate(
                PaginateQuery.Create(paginated.PageIndex, paginated.PageSize),
                s => s.Id,
                PaginateOrder.DESC,
                cancellationToken)
                .Map(data => MapResult(data, paginated));

        return query.PaginateOverPK(
            paginated.PageSize,
            lastId,
            PaginateOrder.DESC,
            cancellationToken)
            .Map(data => MapResult(data, paginated));
    }

    public ValueTask<PrimitiveResult<ProductCategoryListDbQueryResponse[]>> FilterProductCategories(
        string langCode,
        CancellationToken cancellationToken)
    {
        var q = from rc in this.DbContext.ProductCategory
                join rct in this.DbContext.ProductCategoryTranslation
                on new { id = rc.Id, langCode } equals new { id = rct.ProductCategoryId, langCode = rct.LanguageCode } into x
                from a in x.DefaultIfEmpty()
                select new ProductCategoryListDbQueryResponse
                {
                    Id = rc.Id,
                    ParentId = rc.ParentId,
                    CreationDate = rc.CreationDate,
                    Title = a.Title,
                    Description = a.Description,
                    Image = a.Image,
                };
        return q.Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "آیتمی با شناسه مورد نظر پیدا نشد"));
    }

    public ValueTask<PrimitiveResult<ProductDetailsDbQueryResponse>> ProductDetails(
        string langCode,
        int ProductId,
        CancellationToken cancellationToken)
    {
        var query =
            from res in this.DbContext.Product
            .Include(r => r.ProductCategory)
            .Include(r => r.ProductMediaList.OrderByDescending(s => s.Ordinal))
                .ThenInclude(r => r.Media)
            where res.Id == ProductId
            select new
            {
                Product = res,

                // Single translation object
                ProductTranslation = res.ProductTranslations
                                   .Where(rt => rt.LanguageCode == langCode)
                                   .FirstOrDefault(),

                // Category translation object
                ProductCategoryTranslation = res.ProductCategory
                                          .ProductCategoryTranslations
                                          .Where(ct => ct.LanguageCode == langCode)
                                          .FirstOrDefault()
            };
        return query.Run(q => q.FirstOrDefaultAsync(), PrimitiveError.CreateInternal("", ""))
            .Map(res => new ProductDetailsDbQueryResponse
            {
                Id = res.Product.Id,
                Title = res.ProductTranslation?.Title ?? string.Empty,
                Description = res.ProductTranslation?.Description ?? string.Empty,
                ProductCategoryId = res.Product.ProductCategory.Id,
                ProductCategoryTitle = res.ProductCategoryTranslation?.Title ?? string.Empty,
                Image = res.ProductTranslation?.Image ?? string.Empty,
                FileId = res.ProductTranslation?.FileId,
                Price = res.Product.Price,
                Discount = res.Product.Discount,
                GuarantyExpirationTime = res.Product.GuarantyExpirationTime,
                PeriodicServiceInterval = res.Product.PeriodicServiceInterval,
                Deleted = res.Product.Deleted,
                Disabled = res.Product.Disabled,
                CreationDate = res.Product.CreationDate,
                Registered = res.Product.Registered,
                Images = res.Product.ProductMediaList
                    .Select(s => new ProductMediaDbQueryResponse
                    {
                        Id = s.Id,
                        Ordinal = s.Ordinal,
                        Path = s.Media.Path
                    }).ToArray()
            });
    }

    public ValueTask<PrimitiveResult<ProductCategoryListDbQueryResponse>> ProductCategoryDetails(
        string langCode,
        int id,
        CancellationToken cancellationToken)
    {
        var q = from rc in this.DbContext.ProductCategory
                join rct in this.DbContext.ProductCategoryTranslation
                on new { id = rc.Id, langCode } equals new { id = rct.ProductCategoryId, langCode = rct.LanguageCode } into x
                from a in x.DefaultIfEmpty()
                where rc.Id.Equals(id)
                select new ProductCategoryListDbQueryResponse
                {
                    Id = rc.Id,
                    ParentId = rc.ParentId,
                    CreationDate = rc.CreationDate,
                    Title = a.Title,
                    Description = a.Description
                };
        return q.Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "آیتمی با شناسه مورد نظر پیدا نشد"));
    }

    public ValueTask<PrimitiveResult<ProductMediaListDbQueryResponse[]>> GetProductMediaList(
        int productId,
        CancellationToken cancellationToken)
    {
        var q = from p in this.DbContext.Product
                join pm in this.DbContext.ProductMedia on p.Id equals pm.ProductId
                join m in this.DbContext.Media on pm.MediaId equals m.Id
                where p.Id.Equals(productId)
                orderby pm.Ordinal ascending
                select new ProductMediaListDbQueryResponse
                {
                    Id = pm.Id,
                    ProductId = p.Id,
                    MediaId = m.Id,
                    Ordinal = pm.Ordinal,
                    Path = m.Path
                };
        return q.Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "آیتمی با شناسه مورد نظر پیدا نشد"));
    }
}

