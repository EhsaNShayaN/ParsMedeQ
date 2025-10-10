using ParsMedeQ.Application.Features.CartFeature.AddToCartFeature;
using ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CartContracts.AddToCartContract;
using ParsMedeQ.Contracts.CartContracts.MergeCartContract;

namespace ParsMedeQ.Presentation.Features.CartFeatures.AddToCartFeature;
sealed class AddToCartEndpoint : EndpointHandlerBase<
    AddToCartApiRequest,
    AddToCartCommand,
    AddToCartCommandResponse,
    AddToCartApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddToCartEndpoint(
        IPresentationMapper<AddToCartApiRequest, AddToCartCommand> apiRequestMapper
        ) : base(
            Endpoints.Cart.Add,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddCartApiRequestMapper : IPresentationMapper<AddToCartApiRequest, AddToCartCommand>
{
    public async ValueTask<PrimitiveResult<AddToCartCommand>> Map(AddToCartApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddToCartCommand(
                    src.UserId,
                    src.AnonymousId,
                    src.RelatedId,
                    src.TableId,
                    1)));
    }
}