using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.MergeCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.MergeCartFeature;
sealed class MergeCartEndpoint : EndpointHandlerBase<
    MergeCartCommand,
    MergeCartCommandResponse,
    MergeCartApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public MergeCartEndpoint(IPresentationMapper<MergeCartCommandResponse, MergeCartApiResponse> responseMapper) : base(
            Endpoints.Cart.MergeCarts,
            HttpMethod.Post,
            responseMapper)
    { }

    protected override Delegate EndpointDelegate =>
    (
        [AsParameters] Guid anonymousId,
        //MergeCartApiRequest request,
        ISender sender,
        CancellationToken cancellationToken) => this.CallMediatRHandler(
        sender,
        () => ValueTask.FromResult(
            PrimitiveResult.Success(
                new MergeCartCommand(anonymousId))),
        cancellationToken);
}
sealed class MergeCartCommandResponseMapper : IPresentationMapper<MergeCartCommandResponse, MergeCartApiResponse>
{
    public ValueTask<PrimitiveResult<MergeCartApiResponse>> Map(MergeCartCommandResponse src, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new MergeCartApiResponse(src.Changed)));
    }
}