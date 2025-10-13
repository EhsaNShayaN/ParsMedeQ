using ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.DeleteFromCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.RemoveFromCartFeature;
sealed class RemoveFromCartEndpoint : EndpointHandlerBase<
    RemoveFromCartApiRequest,
    RemoveFromCartCommand,
    RemoveFromCartCommandResponse,
    RemoveFromCartApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public RemoveFromCartEndpoint(
        IPresentationMapper<RemoveFromCartApiRequest, RemoveFromCartCommand> requestMapper,
        IPresentationMapper<RemoveFromCartCommandResponse, RemoveFromCartApiResponse> responseMapper) : base(
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
sealed class RemoveFromCartCommandResponseMapper : IPresentationMapper<RemoveFromCartCommandResponse, RemoveFromCartApiResponse>
{
    public ValueTask<PrimitiveResult<RemoveFromCartApiResponse>> Map(RemoveFromCartCommandResponse src, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new RemoveFromCartApiResponse(src.Changed)));
    }
}
