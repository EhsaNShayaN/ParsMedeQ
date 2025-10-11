using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.CartFeature.CartListFeature;
using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.CartListContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.CartList;

sealed class CartListEndpoint : EndpointHandlerBase<
    CartListApiRequest,
    CartListQuery,
    CartListQueryResponse,
    CartListApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public CartListEndpoint(
        IPresentationMapper<CartListApiRequest, CartListQuery> requestMapper,
        IPresentationMapper<CartListQueryResponse, CartListApiResponse> responseMapper)
        : base(
            Endpoints.Cart.List,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] CartListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new CartListQuery(request.AnonymousId))),
            cancellationToken);

}
sealed class CartListApiRequestMapper : IPresentationMapper<
    CartListApiRequest,
    CartListQuery>
{
    public ValueTask<PrimitiveResult<CartListQuery>> Map(
        CartListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new CartListQuery(src.AnonymousId)));
    }
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