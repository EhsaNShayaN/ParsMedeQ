using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Models;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ResourceRepositories;
internal sealed class ResourceReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IResourceReadRepository
{
    public ResourceReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<Resource>>> FilterResources(
        BasePaginatedQuery paginated,
        int tableId,
        int lastId,
        CancellationToken cancellationToken)
    {
        static BasePaginatedApiResponse<Resource> MapResult(
            PaginateListResult<Resource> paginatedData,
            BasePaginatedQuery pageinated)
        {
            var data = paginatedData.Data.ToArray();
            return new BasePaginatedApiResponse<Resource>(
                data,
                paginatedData.Total,
                pageinated.PageIndex,
                pageinated.PageSize)
            {
                LastId = data.Length > 0 ? data.Last().Id : 0
            };
        }

        var baseQuery = this.DbContext
            .Resource
            .Where(a => !a.Deleted.Equals(false));

        if (lastId.Equals(0))
            return baseQuery.Paginate(
                PaginateQuery.Create(paginated.PageIndex, paginated.PageSize),
                s => s.Id,
                PaginateOrder.DESC,
                cancellationToken)
                .Map(data => MapResult(data, paginated));

        return baseQuery.PaginateOverPK(
            paginated.PageSize,
            lastId,
            PaginateOrder.DESC,
            cancellationToken)
            .Map(data => MapResult(data, paginated));
    }

    public ValueTask<PrimitiveResult<ResourceCategory[]>> FilterResourceCategories(
        int TableId,
        CancellationToken cancellationToken)
    {
        return this.DbContext
            .ResourceCategory
            .Where(s => s.TableId.Equals(TableId))
            .Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "دسته بندی ای برای نمایش وجود ندارد."))
            .Map(a => a!);
    }

    public ValueTask<PrimitiveResult<Resource>> ResourceDetails(int Id, CancellationToken cancellationToken)
    {
        return this.DbContext
            .Resource
            .Where(s => s.Id.Equals(Id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "آیتم مورد نظر موجود نمی باشد."))
            .Map(a => a!);
    }

    public ValueTask<PrimitiveResult<ResourceCategory>> ResourceDetailsCategory(int Id, CancellationToken cancellationToken)
    {
        return this.DbContext
            .ResourceCategory
            .Where(s => s.Id.Equals(Id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "آیتم مورد نظر موجود نمی باشد."))
            .Map(a => a!);
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

