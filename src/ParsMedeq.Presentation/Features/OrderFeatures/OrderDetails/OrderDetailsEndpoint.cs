using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.OrderFeatures.OrderDetailsFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.OrderContracts.OrderDetailsContract;
using Order = ParsMedeQ.Domain.Aggregates.OrderAggregate.Order;

namespace ParsMedeQ.Presentation.Features.OrderFeatures.OrderDetails;

sealed class OrderDetailsEndpoint : EndpointHandlerBase<
    OrderDetailsApiRequest,
    OrderDetailsQuery,
    Order,
    OrderDetailsApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public OrderDetailsEndpoint(
        IPresentationMapper<OrderDetailsApiRequest, OrderDetailsQuery> requestMapper,
        IPresentationMapper<Order, OrderDetailsApiResponse> responseMapper)
        : base(
            Endpoints.Order.Details,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] OrderDetailsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new OrderDetailsQuery(request.Id))),
            cancellationToken);

}
sealed class OrderDetailsApiRequestMapper : IPresentationMapper<
    OrderDetailsApiRequest,
    OrderDetailsQuery>
{
    public ValueTask<PrimitiveResult<OrderDetailsQuery>> Map(
        OrderDetailsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new OrderDetailsQuery(src.Id)));
    }
}
sealed class OrderDetailsApiResponseMapper : IPresentationMapper<
    Order,
    OrderDetailsApiResponse>
{
    public ValueTask<PrimitiveResult<OrderDetailsApiResponse>> Map(
        Order src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new OrderDetailsApiResponse(
                        src.Id,
                        src.Payments.First().Id,
                        src.OrderNumber,
                        src.TotalAmount,
                        src.DiscountAmount,
                        src.FinalAmount,
                        src.Status,
                        src.UpdateDate,
                        src.CreationDate,
                        src.OrderItems.Select(s => new OrderItemApiResponse(
                            s.OrderId,
                            s.TableId,
                            s.RelatedId,
                            s.RelatedName,
                            s.Quantity,
                            s.UnitPrice,
                            s.Subtotal)).ToArray())));
    }
}