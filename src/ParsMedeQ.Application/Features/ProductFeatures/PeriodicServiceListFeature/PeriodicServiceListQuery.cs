using ParsMedeQ.Application.Helpers;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.ProductFeatures.PeriodicServiceListFeature;
public sealed record PeriodicServiceListQuery() :
    BasePaginatedQuery,
    IPrimitiveResultQuery<BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>>;

sealed class PeriodicServiceListQueryHandler : IPrimitiveResultQueryHandler<PeriodicServiceListQuery, BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public PeriodicServiceListQueryHandler(IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<PeriodicServiceListDbQueryResponse>>> Handle(PeriodicServiceListQuery request, CancellationToken cancellationToken)
    => await this._readUnitOfWork.ProductReadRepository.FilterPeriodicServices(
            request,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
}