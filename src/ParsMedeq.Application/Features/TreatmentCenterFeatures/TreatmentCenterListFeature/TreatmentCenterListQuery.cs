using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
public sealed record TreatmentCenterListQuery() : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>;

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
            _userLangContextAccessor.GetCurrentLang(),
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
    }
}