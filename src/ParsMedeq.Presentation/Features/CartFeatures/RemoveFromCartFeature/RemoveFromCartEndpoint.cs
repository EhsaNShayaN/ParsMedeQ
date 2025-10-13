using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.DeleteFromCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.RemoveFromCartFeature;
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
        [FromQuery] Guid anonymousId,
        RemoveFromCartApiRequest request,
        ISender sender,
        CancellationToken cancellationToken) => this.CallMediatRHandler(
        sender,
        () => ValueTask.FromResult(
            PrimitiveResult.Success(
                new RemoveFromCartCommand(anonymousId, request.RelatedId))),
        cancellationToken);
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
