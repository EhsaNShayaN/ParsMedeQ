using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
public interface IResourceWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Resource>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Resource>> AddResource(Resource resource, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Resource>> UpdateResource(Resource resource, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory>> FindCategoryById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory>> AddResourceCategory(ResourceCategory resourceCategory, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ResourceCategory>> UpdateResourceCategory(ResourceCategory resourceCategory, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Resource>> Delete(Resource resource, CancellationToken cancellationToken);
}
