using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParsMedeQ.Application.Features.CartFeature.CartListFeature;
using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.CartListContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.CartListFeature;

sealed class CartListEndpoint : EndpointHandlerBase<
    CartListQuery,
    CartListQueryResponse,
    CartListApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public CartListEndpoint(
        IPresentationMapper<CartListQueryResponse, CartListApiResponse> responseMapper)
        : base(
            Endpoints.Cart.Carts,
            HttpMethod.Get,
            responseMapper)
    { }
    protected override Delegate EndpointDelegate =>
    (
        [FromQuery] Guid anonymousId,
        ISender sender,
        CancellationToken cancellationToken) => this.CallMediatRHandler(
        sender,
        () => ValueTask.FromResult(
            PrimitiveResult.Success(
                new CartListQuery(anonymousId))),
        cancellationToken);
}
sealed class CartListApiResponseMapper : IPresentationMapper<
    CartListQueryResponse,
    CartListApiResponse>
{
    public ValueTask<PrimitiveResult<CartListApiResponse>> Map(
        CartListQueryResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new CartListApiResponse(
                    src.Id,
                    src.CartItems.Select(item => new CartItemListApiResponse(
                        item.TableId,
                        item.RelatedId,
                        item.RelatedName,
                        item.UnitPrice,
                        item.Quantity)).ToArray())));
    }
}