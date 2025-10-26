using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.TicketFeatures.TicketListFeature;
public sealed record TicketListQuery(int? RelatedId, bool? IsAdmin) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<TicketListDbQueryResponse>>;

sealed class TicketListQueryHandler : IPrimitiveResultQueryHandler<TicketListQuery, BasePaginatedApiResponse<TicketListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public TicketListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<TicketListDbQueryResponse>>> Handle(TicketListQuery request, CancellationToken cancellationToken)
    {
        int? userId = !request.IsAdmin.HasValue || request.IsAdmin.Value ? null : this._userContextAccessor.Current.UserId;
        return await this._readUnitOfWork.TicketReadRepository.FilterTickets(
            request,
            userId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
    }
}