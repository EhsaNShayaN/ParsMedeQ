using ParsMedeQ.Application.Persistance.Schema.LocationRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.LocationRepositories;
internal sealed class LocationWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ILocationWriteRepository
{
    public LocationWriteRepository(WriteDbContext dbContext) : base(dbContext) { }
}
