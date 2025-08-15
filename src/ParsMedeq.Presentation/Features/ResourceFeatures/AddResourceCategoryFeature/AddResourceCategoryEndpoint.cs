using ParsMedeQ.Application.Features.ResourceFeatures.AddResourceCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.AddResourceCategoryContract;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.AddResourceCategoryFeature;
sealed class AddResourceCategoryEndpoint : EndpointHandlerBase<
    AddResourceCategoryApiRequest,
    AddResourceCategoryCommand,
    AddResourceCategoryCommandResponse,
    AddResourceCategoryApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddResourceCategoryEndpoint(
        IPresentationMapper<AddResourceCategoryApiRequest, AddResourceCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Resource.AddResourceCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddResourceCategoryApiRequestMapper : IPresentationMapper<AddResourceCategoryApiRequest, AddResourceCategoryCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCategoryCommand>> Map(AddResourceCategoryApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddResourceCategoryCommand(
                    src.TableId,
                    src.Title,
                    src.Description,
                    src.ParentId)));
}