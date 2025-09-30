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
                Title = res.ProductTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                ProductCategoryTitle = res.ProductCategory.ProductCategoryTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                Image = res.ProductTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,

                Id = res.Id,
                Title = res.Title,
                ProductCategoryId = res.ProductCategoryId,
                ProductCategoryTitle = res.ProductCategoryTitle,
                Image = res.Image,
                Price = res.Price,
                Discount = res.Discount,
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
        int UserId,
        int ProductId,
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
                ProductCategoryId = res.Product.ProductCategory.Id,
                ProductCategoryTitle = res.ProductCategoryTranslation?.Title ?? string.Empty,
                Image = res.ProductTranslation?.Image ?? string.Empty,
                FileId = res.ProductTranslation?.FileId,
                Price = res.Product.Price,
                Discount = res.Product.Discount,
                Deleted = res.Product.Deleted,
                Disabled = res.Product.Disabled,
                CreationDate = res.Product.CreationDate,
                Registered = res.Product.Registered
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

