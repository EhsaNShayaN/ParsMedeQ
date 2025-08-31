using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ResourceRepositories;
internal sealed class ResourceWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IResourceWriteRepository
{
    public ResourceWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Resource>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Resource, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<Resource>> AddResource(Resource resource, CancellationToken cancellationToken) =>
        this.Add(resource);
    public ValueTask<PrimitiveResult<Resource>> UpdateResource(Resource resource, CancellationToken cancellationToken) =>
        this.Update(resource);
    public ValueTask<PrimitiveResult<ResourceCategory>> FindCategoryById(int id, CancellationToken cancellationToken)
    {
        return this.DbContext
            .ResourceCategory
            .Include(s => s.ResourceCategoryTranslations)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "دسته بندی با شناسه مورد نظر پیدا نشد"));
    }
    public ValueTask<PrimitiveResult<ResourceCategory>> AddResourceCategory(ResourceCategory resourceCategory, CancellationToken cancellationToken) =>
        this.Add(resourceCategory);
    public ValueTask<PrimitiveResult<ResourceCategory>> UpdateResourceCategory(ResourceCategory resourceCategory, CancellationToken cancellationToken) =>
        this.Update(resourceCategory);
}
