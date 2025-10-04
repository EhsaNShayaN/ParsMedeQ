using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.MediaRepositories;
internal sealed class MediaWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IMediaWriteRepository
{
    public MediaWriteRepository(WriteDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<Media>> AddMedia(Media media) => this.Add(media);
}
