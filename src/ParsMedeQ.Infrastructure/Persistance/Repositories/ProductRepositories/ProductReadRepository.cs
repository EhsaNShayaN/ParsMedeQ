using ParsMedeQ.Application.Features.ProductFeatures.ProductCategoryListFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductDetailsFeature;
using ParsMedeQ.Application.Features.ProductFeatures.ProductListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Models;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ProductRepositories;
internal sealed class ProductReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IProductReadRepository
{
    public ProductReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<ProductListDbQueryResponse>>> FilterProducts(
        BasePaginatedQuery paginated,
        string langCode,
        int productCategoryId,
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
            .Where(res => res.ProductCategoryId.Equals(productCategoryId))
            .Include(r => r.ProductTranslations.Where(l => l.LanguageCode == langCode))
            .Include(r => r.ProductCategory)
            .ThenInclude(r => r.ProductCategoryTranslations.Where(l => l.LanguageCode == langCode))
            where res.Deleted == false
            select new ProductListDbQueryResponse
            {
                Id = res.Id,
                Title = res.ProductTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                ProductCategoryId = res.ProductCategoryId,
                ProductCategoryTitle = res.ProductCategory.ProductCategoryTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                Image = res.ProductTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,
                Language = res.Language,
                Price = res.Price,
                Discount = res.Discount,
                IsVip = res.IsVip,
                DownloadCount = res.DownloadCount,
                Ordinal = res.Ordinal,
                Deleted = res.Deleted,
                Disabled = res.Disabled,
                ExpirationDate = res.ExpirationDate,
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
                    Description = a.Description
                };
        return q.Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "آیتمی با شناسه مورد نظر پیدا نشد"));
    }

    public ValueTask<PrimitiveResult<ProductDetailsDbQueryResponse>> ProductDetails(
        string langCode,
        int UserId,
        int ProductId,
        int TableId,
        CancellationToken cancellationToken)
    {
        var query =
            from res in this.DbContext.Product.Include(r => r.ProductCategory)
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
                Abstract = res.ProductTranslation?.Abstract ?? string.Empty,
                Anchors = res.ProductTranslation?.Anchors ?? string.Empty,
                Keywords = res.ProductTranslation?.Keywords ?? string.Empty,
                ProductCategoryId = res.Product.ProductCategory.Id,
                ProductCategoryTitle = res.ProductCategoryTranslation?.Title ?? string.Empty,
                Image = res.ProductTranslation?.Image ?? string.Empty,
                FileId = res.ProductTranslation?.FileId,
                Language = res.Product.Language,
                PublishDate = res.Product.PublishDate,
                PublishInfo = res.Product.PublishInfo,
                Publisher = res.Product.Publisher,
                Price = res.Product.Price,
                Discount = res.Product.Discount,
                IsVip = res.Product.IsVip,
                DownloadCount = res.Product.DownloadCount,
                Ordinal = res.Product.Ordinal,
                Deleted = res.Product.Deleted,
                Disabled = res.Product.Disabled,
                ExpirationDate = res.Product.ExpirationDate,
                CreationDate = res.Product.CreationDate,
                Registered = res.Product.Registered/*,
                ProductCategories = (
                    from rel in this.DbContext.ProductCategoryRelations
                    join cat in this.DbContext.ProductCategory on rel.ProductCategoryId equals cat.Id
                    where rel.Id == res.Product.Id
                    select new ProductCategoryDbQueryResponse(cat.Id, string.Empty)
                ).ToArray()*/
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
}

