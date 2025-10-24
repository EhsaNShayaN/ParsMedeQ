using ParsMedeQ.Application.Helpers;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.TicketFeatures.TicketListFeature;
public sealed record TicketListQuery() : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<TicketListDbQueryResponse>>;

sealed class TicketListQueryHandler : IPrimitiveResultQueryHandler<TicketListQuery, BasePaginatedApiResponse<TicketListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public TicketListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<TicketListDbQueryResponse>>> Handle(TicketListQuery request, CancellationToken cancellationToken)
    => await this._readUnitOfWork.TicketReadRepository.FilterTickets(
            request,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
}