using ParsMedeQ.Application.Features.OrderFeatures.OrderListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.OrderContracts.OrderListContract;
using ParsMedeQ.Domain;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.OrderFeatures.OrderList;

sealed class OrderListEndpoint : EndpointHandlerBase<
    OrderListApiRequest,
    OrderListQuery,
    BasePaginatedApiResponse<OrderListDbQueryResponse>,
    BasePaginatedApiResponse<OrderListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public OrderListEndpoint(
        IPresentationMapper<OrderListApiRequest, OrderListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<OrderListDbQueryResponse>, BasePaginatedApiResponse<OrderListApiResponse>> responseMapper)
        : base(
            Endpoints.Order.Orders,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class OrderListApiRequestMapper : IPresentationMapper<
    OrderListApiRequest,
    OrderListQuery>
{
    public ValueTask<PrimitiveResult<OrderListQuery>> Map(
        OrderListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new OrderListQuery(null)
                {
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class OrderListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<OrderListDbQueryResponse>,
    BasePaginatedApiResponse<OrderListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<OrderListApiResponse>>> Map(
        BasePaginatedApiResponse<OrderListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<OrderListApiResponse>(src.Items.Select(data =>
                    new OrderListApiResponse(
                        data.Id,
                        data.UserId,
                        data.OrderNumber,
                        data.TotalAmount,
                        data.DiscountAmount,
                        data.FinalAmount,
                        data.Status,
                        ((OrderStatus)data.Status).GetDescription(),
                        data.UpdateDate,
                        data.CreationDate))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}