using ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
public interface IResourceReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<BasePaginatedApiResponse<ResourceListDbQueryResponse>>> FilterResources(
        BasePaginatedQuery paginated,
        int tableId,
        int lastId,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategoryListDbQueryResponse[]>> FilterResourceCategories(
        string langCode,
        int tableId,
        CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<ResourceDetailsDbQueryResponse>> ResourceDetails(
        int UserId,
        int ResourceId,
        int TableId,
        CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<ResourceCategoryListDbQueryResponse>> ResourceCategoryDetails(
        string langCode,
        int id,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategoryRelations[]>> FilterResourceCategoryRelations(
        int resourceId,
        CancellationToken cancellationToken);
}
