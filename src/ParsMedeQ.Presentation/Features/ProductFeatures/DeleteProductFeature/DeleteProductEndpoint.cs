using ParsMedeQ.Application.Features.ProductFeatures.DeleteProductFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.DeleteProductContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.DeleteProductFeature;
sealed class DeleteProductEndpoint : EndpointHandlerBase<
    DeleteProductApiRequest,
    DeleteProductCommand,
    DeleteProductCommandResponse,
    DeleteProductApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public DeleteProductEndpoint(
        IPresentationMapper<DeleteProductApiRequest, DeleteProductCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.DeleteProduct,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class DeleteProductApiRequestMapper : IPresentationMapper<DeleteProductApiRequest, DeleteProductCommand>
{
    public ValueTask<PrimitiveResult<DeleteProductCommand>> Map(DeleteProductApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new DeleteProductCommand(src.Id)));
}