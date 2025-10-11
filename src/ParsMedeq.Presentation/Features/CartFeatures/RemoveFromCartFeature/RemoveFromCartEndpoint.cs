using ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.DeleteFromCartContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.RemoveFromCartFeature;
sealed class RemoveFromCartEndpoint : EndpointHandlerBase<
    RemoveFromCartApiRequest,
    RemoveFromCartCommand,
    RemoveFromCartCommandResponse,
    RemoveFromCartApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public RemoveFromCartEndpoint(
        IPresentationMapper<RemoveFromCartApiRequest, RemoveFromCartCommand> apiRequestMapper
        ) : base(
            Endpoints.Cart.Remove,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class RemoveFromCartApiRequestMapper : IPresentationMapper<RemoveFromCartApiRequest, RemoveFromCartCommand>
{
    public ValueTask<PrimitiveResult<RemoveFromCartCommand>> Map(RemoveFromCartApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new RemoveFromCartCommand(
                    src.AnonymousId,
                    src.RelatedId)));
}