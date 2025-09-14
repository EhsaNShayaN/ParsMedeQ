using ParsMedeQ.Application.Features.ProductFeatures.AddProductCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.AddProductCategoryContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.AddProductCategoryFeature;
sealed class AddProductCategoryEndpoint : EndpointHandlerBase<
    AddProductCategoryApiRequest,
    AddProductCategoryCommand,
    AddProductCategoryCommandResponse,
    AddProductCategoryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddProductCategoryEndpoint(
        IPresentationMapper<AddProductCategoryApiRequest, AddProductCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.AddProductCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddProductCategoryApiRequestMapper : IPresentationMapper<AddProductCategoryApiRequest, AddProductCategoryCommand>
{
    public ValueTask<PrimitiveResult<AddProductCategoryCommand>> Map(AddProductCategoryApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddProductCategoryCommand(
                    src.TableId,
                    src.Title,
                    src.Description,
                    src.ParentId)));
}