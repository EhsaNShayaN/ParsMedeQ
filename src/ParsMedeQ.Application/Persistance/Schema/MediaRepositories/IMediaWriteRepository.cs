using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
public interface IMediaWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Media>> AddMedia(Media Media, CancellationToken cancellationToken);
}
