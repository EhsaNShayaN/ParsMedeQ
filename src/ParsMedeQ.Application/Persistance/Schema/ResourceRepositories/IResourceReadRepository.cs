using ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
public interface IResourceReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<BasePaginatedApiResponse<Resource>>> FilterResources(
        BasePaginatedQuery paginated,
        int tableId,
        int lastId,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory[]>> FilterResourceCategories(
        int tableId,
        CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<Resource>> ResourceDetails(
        int id,
        CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<ResourceDetailsDbQueryResponse>> ResourceDetails(
        int UserId,
        int ResourceId,
        int TableId,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory>> ResourceCategoryDetails(
        int id,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategoryRelations[]>> FilterResourceCategoryRelations(
        int resourceId,
        CancellationToken cancellationToken);
}
