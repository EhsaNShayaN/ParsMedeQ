using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.DeleteFromCartContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.RemoveFromCartFeature;
sealed class RemoveFromCartEndpoint : EndpointHandlerBase<
    RemoveFromCartCommand,
    RemoveFromCartCommandResponse,
    RemoveFromCartApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public RemoveFromCartEndpoint(
                IPresentationMapper<RemoveFromCartCommandResponse, RemoveFromCartApiResponse> responseMapper) : base(
            Endpoints.Cart.RemoveCart,
            HttpMethod.Post,
            responseMapper)
    { }

    protected override Delegate EndpointDelegate =>
    (
        [AsParameters] Guid? anonymousId,
        RemoveFromCartApiRequest request,
        ISender sender,
        CancellationToken cancellationToken) => this.CallMediatRHandler(
        sender,
        () => ValueTask.FromResult(
            PrimitiveResult.Success(
                new RemoveFromCartCommand(anonymousId, request.RelatedId))),
        cancellationToken);
}
