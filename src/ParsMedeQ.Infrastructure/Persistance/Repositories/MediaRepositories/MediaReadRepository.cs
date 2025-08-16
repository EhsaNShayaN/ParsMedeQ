using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.MediaRepositories;
internal sealed class MediaReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IMediaReadRepository
{
    public MediaReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<Media>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Media, int>(id, cancellationToken);
}

