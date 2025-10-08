using ParsMedeQ.Application.Features.ProductFeatures.DeleteProductCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.DeleteProductCategoryContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.DeleteProductCategoryFeature;
sealed class DeleteProductCategoryEndpoint : EndpointHandlerBase<
    DeleteProductCategoryApiRequest,
    DeleteProductCategoryCommand,
    DeleteProductCategoryCommandResponse,
    DeleteProductCategoryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public DeleteProductCategoryEndpoint(
        IPresentationMapper<DeleteProductCategoryApiRequest, DeleteProductCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.DeleteProductCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class DeleteProductCategoryApiRequestMapper : IPresentationMapper<DeleteProductCategoryApiRequest, DeleteProductCategoryCommand>
{
    public ValueTask<PrimitiveResult<DeleteProductCategoryCommand>> Map(DeleteProductCategoryApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new DeleteProductCategoryCommand(src.Id)));
}