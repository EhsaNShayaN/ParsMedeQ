using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterDetailsFeature;
using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
public interface ITreatmentCenterReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<TreatmentCenterDetailsDbQueryResponse>> FindById(int id, string langCode, CancellationToken cancellationToken);
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>> FilterTreatmentCenters(
        BasePaginatedQuery paginated,
        string langCode,
        string query,
        int provinceId,
        int cityId,
        int lastId,
        CancellationToken cancellationToken);
}
