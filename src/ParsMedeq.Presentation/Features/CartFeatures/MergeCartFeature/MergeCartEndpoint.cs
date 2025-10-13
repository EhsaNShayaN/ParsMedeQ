using ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.CartListContract;
using ParsMedeQ.Contracts.CartContracts.MergeCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.MergeCartFeature;
sealed class MergeCartEndpoint : EndpointHandlerBase<
    MergeCartApiRequest,
    MergeCartCommand,
    MergeCartCommandResponse,
    MergeCartApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public MergeCartEndpoint(
        IPresentationMapper<MergeCartApiRequest, MergeCartCommand> requestMapper,
        IPresentationMapper<MergeCartCommandResponse, MergeCartApiResponse> responseMapper) : base(
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
sealed class MergeCartCommandResponseMapper : IPresentationMapper<MergeCartCommandResponse, MergeCartApiResponse>
{
    public ValueTask<PrimitiveResult<MergeCartApiResponse>> Map(MergeCartCommandResponse src, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new MergeCartApiResponse(src.Changed)));
    }
}