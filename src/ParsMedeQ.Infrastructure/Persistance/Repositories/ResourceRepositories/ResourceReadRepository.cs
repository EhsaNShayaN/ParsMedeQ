using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.ResourceRepositories;
internal sealed class ResourceReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IResourceReadRepository
{
    public ResourceReadRepository(ReadDbContext dbContext) : base(dbContext) { }
}

