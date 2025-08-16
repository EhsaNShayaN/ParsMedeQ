using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
public interface IMediaReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Media>> FindById(int id, CancellationToken cancellationToken);
}
