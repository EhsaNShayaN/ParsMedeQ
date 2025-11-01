using ParsMedeQ.Application.Services.UserLangServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterDetailsFeature;
public sealed record TreatmentCenterDetailsQuery(int Id) : IPrimitiveResultQuery<TreatmentCenterDetailsDbQueryResponse>;

sealed class TreatmentCenterDetailsQueryHandler : IPrimitiveResultQueryHandler<TreatmentCenterDetailsQuery, TreatmentCenterDetailsDbQueryResponse>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public TreatmentCenterDetailsQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userLangContextAccessor = userLangContextAccessor;
    }
    public async Task<PrimitiveResult<TreatmentCenterDetailsDbQueryResponse>> Handle(TreatmentCenterDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.TreatmentCenterReadRepository.FindById(
            request.Id,
            this._userLangContextAccessor.GetCurrentLang(),
            cancellationToken)
        .ConfigureAwait(false);
}