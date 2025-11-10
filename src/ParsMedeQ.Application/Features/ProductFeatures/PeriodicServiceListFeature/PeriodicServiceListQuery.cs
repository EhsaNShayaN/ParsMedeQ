using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductFeatures.PeriodicServiceListFeature;
public sealed record PeriodicServiceListQuery(bool? IsAdmin) :
    BasePaginatedQuery,
    IPrimitiveResultQuery<BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>>;

sealed class PeriodicServiceListQueryHandler : IPrimitiveResultQueryHandler<PeriodicServiceListQuery, BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public PeriodicServiceListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>>> Handle(PeriodicServiceListQuery request, CancellationToken cancellationToken)
    {
        var userId = !request.IsAdmin.HasValue || request.IsAdmin.Value ? 0 : this._userContextAccessor.Current.UserId;
        return await this._readUnitOfWork.ProductReadRepository.FilterPeriodicServices(
            request,
            userId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
    }
}