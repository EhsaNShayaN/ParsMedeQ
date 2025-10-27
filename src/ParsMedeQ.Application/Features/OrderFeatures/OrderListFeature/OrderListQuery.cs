using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.OrderFeatures.OrderListFeature;
public sealed record OrderListQuery(bool? IsAdmin) : BasePaginatedQuery, IPrimitiveResultQuery<BasePaginatedApiResponse<OrderListDbQueryResponse>>;

sealed class OrderListQueryHandler : IPrimitiveResultQueryHandler<OrderListQuery, BasePaginatedApiResponse<OrderListDbQueryResponse>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public OrderListQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<BasePaginatedApiResponse<OrderListDbQueryResponse>>> Handle(OrderListQuery request, CancellationToken cancellationToken)
    {
        var userId = !request.IsAdmin.HasValue || request.IsAdmin.Value ? 0 : this._userContextAccessor.Current.UserId;
        return await this._readUnitOfWork.OrderReadRepository.FilterOrders(
            request,
            userId,
            request.LastId,
            cancellationToken)
        .ConfigureAwait(false);
    }
}