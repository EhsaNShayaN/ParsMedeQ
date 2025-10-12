using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.CartFeature.AddToCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.AddToCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.AddToCartFeature;
sealed class AddToCartEndpoint : EndpointHandlerBase<
    AddToCartCommand,
    AddToCartCommandResponse,
    AddToCartApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddToCartEndpoint(IPresentationMapper<AddToCartCommandResponse, AddToCartApiResponse> responseMapper) : base(
            Endpoints.Cart.AddCart,
            HttpMethod.Post,
            responseMapper)
    { }

    protected override Delegate EndpointDelegate =>
    (
        [AsParameters] Guid anonymousId,
        AddToCartApiRequest request,
        ISender sender,
        CancellationToken cancellationToken) => this.CallMediatRHandler(
        sender,
        () => ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddToCartCommand(anonymousId, request.RelatedId, request.TableId, request.Quantity))),
        cancellationToken);
}
sealed class AddToCartCommandResponseMapper : IPresentationMapper<AddToCartCommandResponse, AddToCartApiResponse>
{
    public ValueTask<PrimitiveResult<AddToCartApiResponse>> Map(AddToCartCommandResponse src, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddToCartApiResponse(src.Changed)));
    }
}