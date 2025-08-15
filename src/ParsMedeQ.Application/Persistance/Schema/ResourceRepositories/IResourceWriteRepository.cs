using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
public interface IResourceWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Resource>> AddResource(Resource Resource, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory>> AddResourceCategory(ResourceCategory Resource, CancellationToken cancellationToken);
}
