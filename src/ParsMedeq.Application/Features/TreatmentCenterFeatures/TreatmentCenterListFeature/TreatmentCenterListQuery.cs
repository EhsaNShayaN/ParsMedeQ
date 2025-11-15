using ParsMedeQ.Application.Helpers;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
public sealed record TreatmentCenterListQuery(
    string Query,
    int ProvinceId,
    int CityId) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>;

sealed class TreatmentCenterListQueryHandler : IPrimitiveResultQueryHandler<TreatmentCenterListQuery, BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public TreatmentCenterListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userLangContextAccessor = userLangContextAccessor;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>> Handle(TreatmentCenterListQuery request, CancellationToken cancellationToken)
    {
        return await this._readUnitOfWork.TreatmentCenterReadRepository.FilterTreatmentCenters(
            request,
            this._userLangContextAccessor.GetCurrentLang(),
            request.Query,
            request.ProvinceId,
            request.CityId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
    }
}