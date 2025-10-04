using ParsMedeQ.Application.Features.ProductFeatures.DeleteProductMediaFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.DeleteProductMediaContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.DeleteProductMediaFeature;
sealed class DeleteProductMediaEndpoint : EndpointHandlerBase<
    DeleteProductMediaApiRequest,
    DeleteProductMediaCommand,
    DeleteProductMediaCommandResponse,
    DeleteProductMediaApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public DeleteProductMediaEndpoint(
        IPresentationMapper<DeleteProductMediaApiRequest, DeleteProductMediaCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.DeleteProductMedia,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class DeleteProductMediaApiRequestMapper : IPresentationMapper<DeleteProductMediaApiRequest, DeleteProductMediaCommand>
{
    public async ValueTask<PrimitiveResult<DeleteProductMediaCommand>> Map(DeleteProductMediaApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new DeleteProductMediaCommand(
                    src.ProductId,
                    src.MediaId)));
    }
}