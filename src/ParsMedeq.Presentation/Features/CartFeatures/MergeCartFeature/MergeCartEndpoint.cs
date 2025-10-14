using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.CartListContract;
using ParsMedeQ.Contracts.CartContracts.MergeCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.MergeCartFeature;
sealed class MergeCartEndpoint : EndpointHandlerBase<
    MergeCartApiRequest,
    MergeCartCommand,
    CartListQueryResponse,
    CartListApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public MergeCartEndpoint(
        IPresentationMapper<MergeCartApiRequest, MergeCartCommand> requestMapper,
        IPresentationMapper<CartListQueryResponse, CartListApiResponse> responseMapper) : base(
            Endpoints.Cart.MergeCarts,
            HttpMethod.Post,
            requestMapper,
            responseMapper)
    { }
}
internal sealed class MergeCartApiRequestMapper : IPresentationMapper<MergeCartApiRequest, MergeCartCommand>
{
    public async ValueTask<PrimitiveResult<MergeCartCommand>> Map(MergeCartApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(PrimitiveResult.Success(new MergeCartCommand(src.AnonymousId)));
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