using ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
using ParsMedeQ.Contracts;
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
        IPresentationMapper<MergeCartApiRequest, MergeCartCommand> apiRequestMapper
        ) : base(
            Endpoints.Cart.MergeCarts,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddCartApiRequestMapper : IPresentationMapper<MergeCartApiRequest, MergeCartCommand>
{
    public async ValueTask<PrimitiveResult<MergeCartCommand>> Map(MergeCartApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new MergeCartCommand(src.AnonymousId)));
    }
}