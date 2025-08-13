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
        int TableId,
        int LastId,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory[]>> FilterResourceCategories(
        int TableId,
        CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<Resource>> ResourceDetails(
        int Id,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory>> ResourceDetailsCategory(
        int Id,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategoryRelations[]>> FilterResourceCategoryRelations(
        int ResourceId,
        CancellationToken cancellationToken);
}
