using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.CartListContract;
using ParsMedeQ.Contracts.CartContracts.DeleteFromCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.RemoveFromCartFeature;
sealed class RemoveFromCartEndpoint : EndpointHandlerBase<
    RemoveFromCartApiRequest,
    RemoveFromCartCommand,
    CartListQueryResponse,
    CartListApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public RemoveFromCartEndpoint(
        IPresentationMapper<RemoveFromCartApiRequest, RemoveFromCartCommand> requestMapper,
        IPresentationMapper<CartListQueryResponse, CartListApiResponse> responseMapper) : base(
            Endpoints.Cart.RemoveCart,
            HttpMethod.Post,
            requestMapper,
            responseMapper)
    { }
}
internal sealed class RemoveFromCartApiRequestMapper : IPresentationMapper<RemoveFromCartApiRequest, RemoveFromCartCommand>
{
    public async ValueTask<PrimitiveResult<RemoveFromCartCommand>> Map(RemoveFromCartApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new RemoveFromCartCommand(
                    src.AnonymousId,
                    src.RelatedId)));
    }
}
sealed class CartListQueryResponseMapper : IPresentationMapper<CartListQueryResponse, CartListApiResponse>
{
    public ValueTask<PrimitiveResult<CartListApiResponse>> Map(CartListQueryResponse src, CancellationToken cancellationToken)
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