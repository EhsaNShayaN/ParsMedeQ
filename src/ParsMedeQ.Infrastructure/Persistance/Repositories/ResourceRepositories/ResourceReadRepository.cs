using ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Models;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ResourceRepositories;
internal sealed class ResourceReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IResourceReadRepository
{
    public ResourceReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<ResourceListDbQueryResponse>>> FilterResources(
        BasePaginatedQuery paginated,
        string langCode,
        int tableId,
        int? resourceCategoryId,
        int lastId,
        CancellationToken cancellationToken)
    {
        static BasePaginatedApiResponse<ResourceListDbQueryResponse> MapResult(
            PaginateListResult<ResourceListDbQueryResponse> paginatedData,
            BasePaginatedQuery pageinated)
        {
            var data = paginatedData.Data.ToArray();
            return new BasePaginatedApiResponse<ResourceListDbQueryResponse>(
                data,
                paginatedData.Total,
                pageinated.PageIndex,
                pageinated.PageSize)
            {
                LastId = data.Length > 0 ? data.Last().Id : 0
            };
        }

        var query =
            from res in this.DbContext.Resource
            .Where(res => res.TableId.Equals(tableId))
            .Include(r => r.ResourceTranslations.Where(l => l.LanguageCode == langCode))
            .Include(r => r.ResourceCategory)
            .ThenInclude(r => r.ResourceCategoryTranslations.Where(l => l.LanguageCode == langCode))
            where res.Deleted == false && ((resourceCategoryId ?? 0) == 0 || res.ResourceCategoryId.Equals(resourceCategoryId))
            select new ResourceListDbQueryResponse
            {
                Id = res.Id,
                TableId = res.TableId,
                Title = res.ResourceTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                ResourceCategoryId = res.ResourceCategoryId,
                ResourceCategoryTitle = res.ResourceCategory.ResourceCategoryTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                Image = res.ResourceTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,
                Language = res.Language,
                Price = res.Price,
                Discount = res.Discount,
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

    public ValueTask<PrimitiveResult<ResourceCategoryListDbQueryResponse[]>> FilterResourceCategories(
        string langCode,
        int tableId,
        CancellationToken cancellationToken)
    {
        var q = from rc in this.DbContext.ResourceCategory
                join rct in this.DbContext.ResourceCategoryTranslation
                on new { id = rc.Id, langCode } equals new { id = rct.ResourceCategoryId, langCode = rct.LanguageCode } into x
                from a in x.DefaultIfEmpty()
                where rc.TableId.Equals(tableId)
                select new ResourceCategoryListDbQueryResponse
                {
                    Id = rc.Id,
                    TableId = rc.TableId,
                    Count = rc.Count,
                    ParentId = rc.ParentId,
                    CreationDate = rc.CreationDate,
                    Title = a.Title,
                    Description = a.Description
                };
        return q.Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "آیتمی با شناسه مورد نظر پیدا نشد"));
    }

    public ValueTask<PrimitiveResult<ResourceDetailsDbQueryResponse>> ResourceDetails(
        string langCode,
        int UserId,
        int resourceId,
        int TableId,
        CancellationToken cancellationToken)
    {
        var query =
            from res in this.DbContext.Resource.Include(r => r.ResourceCategory)
            where res.Id == resourceId
            select new
            {
                Resource = res,

                // Single translation object
                ResourceTranslation = res.ResourceTranslations
                                   .Where(rt => rt.LanguageCode == langCode)
                                   .FirstOrDefault(),

                // Category translation object
                ResourceCategoryTranslation = res.ResourceCategory
                                          .ResourceCategoryTranslations
                                          .Where(ct => ct.LanguageCode == langCode)
                                          .FirstOrDefault()
            };
        return query.Run(q => q.FirstOrDefaultAsync(), PrimitiveError.CreateInternal("", ""))
            .Map(res => new ResourceDetailsDbQueryResponse
            {
                Id = res.Resource.Id,
                TableId = res.Resource.TableId,
                Title = res.ResourceTranslation?.Title ?? string.Empty,
                Description = res.ResourceTranslation?.Description ?? string.Empty,
                Abstract = res.ResourceTranslation?.Abstract ?? string.Empty,
                Anchors = res.ResourceTranslation?.Anchors ?? string.Empty,
                Keywords = res.ResourceTranslation?.Keywords ?? string.Empty,
                ResourceCategoryId = res.Resource.ResourceCategory.Id,
                ResourceCategoryTitle = res.ResourceCategoryTranslation?.Title ?? string.Empty,
                Image = res.ResourceTranslation?.Image ?? string.Empty,
                FileId = res.ResourceTranslation?.FileId,
                Language = res.Resource.Language,
                PublishDate = res.Resource.PublishDate,
                PublishInfo = res.Resource.PublishInfo,
                Publisher = res.Resource.Publisher,
                Price = res.Resource.Price,
                Discount = res.Resource.Discount,
                DownloadCount = res.Resource.DownloadCount,
                Ordinal = res.Resource.Ordinal,
                Deleted = res.Resource.Deleted,
                Disabled = res.Resource.Disabled,
                ExpirationDate = res.Resource.ExpirationDate,
                CreationDate = res.Resource.CreationDate,
                Registered = res.Resource.Registered,
                /*ResourceCategories = (
                    from rel in this.DbContext.ResourceCategoryRelations
                    join cat in this.DbContext.ResourceCategory on rel.ResourceCategoryId equals cat.Id
                    where rel.Id == res.Resource.Id
                    select new ResourceCategoryDbQueryResponse(cat.Id, string.Empty)
                ).ToArray()*/
            });
    }

    public ValueTask<PrimitiveResult<ResourceCategoryListDbQueryResponse>> ResourceCategoryDetails(
        string langCode,
        int id,
        CancellationToken cancellationToken)
    {
        var q = from rc in this.DbContext.ResourceCategory
                join rct in this.DbContext.ResourceCategoryTranslation
                on new { id = rc.Id, langCode } equals new { id = rct.ResourceCategoryId, langCode = rct.LanguageCode } into x
                from a in x.DefaultIfEmpty()
                where rc.Id.Equals(id)
                select new ResourceCategoryListDbQueryResponse
                {
                    Id = rc.Id,
                    TableId = rc.TableId,
                    Count = rc.Count,
                    ParentId = rc.ParentId,
                    CreationDate = rc.CreationDate,
                    Title = a.Title,
                    Description = a.Description
                };
        return q.Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "آیتمی با شناسه مورد نظر پیدا نشد"));
    }

    public ValueTask<PrimitiveResult<ResourceCategoryRelations[]>> FilterResourceCategoryRelations(
        int ResourceId,
        CancellationToken cancellationToken)
    {
        return this.DbContext
            .ResourceCategoryRelations
            .Where(s => s.ResourceId.Equals(ResourceId))
            .Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "دسته بندی ای برای نمایش وجود ندارد."))
            .Map(a => a!);
    }
}

