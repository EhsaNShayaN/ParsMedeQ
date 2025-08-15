using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ResourceRepositories;
internal sealed class ResourceWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IResourceWriteRepository
{
    public ResourceWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Resource>> AddResource(Resource Resource, CancellationToken cancellationToken)
        => this.Add(Resource);
    public ValueTask<PrimitiveResult<ResourceCategory>> AddResourceCategory(ResourceCategory ResourceCategory, CancellationToken cancellationToken)
        => this.Add(ResourceCategory);
}
