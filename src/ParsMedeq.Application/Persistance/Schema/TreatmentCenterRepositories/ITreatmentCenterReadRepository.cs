using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
public interface ITreatmentCenterReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<TreatmentCenter>> FindById(int id, CancellationToken cancellationToken);
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>> FilterTreatmentCenters(
        BasePaginatedQuery paginated,
        string langCode,
        int lastId,
        CancellationToken cancellationToken);
}
