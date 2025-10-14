using ParsMedeQ.Application.Features.CartFeature.AddToCartFeature;
using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.AddToCartContract;
using ParsMedeQ.Contracts.CartContracts.CartListContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.AddToCartFeature;
sealed class AddToCartEndpoint : EndpointHandlerBase<
    AddToCartApiRequest,
    AddToCartCommand,
    CartListQueryResponse,
    CartListApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddToCartEndpoint(
        IPresentationMapper<AddToCartApiRequest, AddToCartCommand> requestMapper,
        IPresentationMapper<CartListQueryResponse, CartListApiResponse> responseMapper) : base(
            Endpoints.Cart.AddCart,
            HttpMethod.Post,
            requestMapper,
            responseMapper)
    { }
}
internal sealed class AddToCartApiRequestMapper : IPresentationMapper<AddToCartApiRequest, AddToCartCommand>
{
    public async ValueTask<PrimitiveResult<AddToCartCommand>> Map(AddToCartApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddToCartCommand(
                    src.AnonymousId,
                    src.RelatedId,
                    src.TableId,
                    src.Quantity)));
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