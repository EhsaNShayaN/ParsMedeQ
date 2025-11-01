using ParsMedeQ.Application.Features.LcationFeatures.LcationListFeature;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.LocationRepositories;
public interface ILocationReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<LocationListDbQueryResponse[]>> FilterLocations(string langCode, CancellationToken cancellationToken);
}
