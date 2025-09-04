using ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.UpdateResourceCategoryContract;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.EditResourceCategoryFeature;
sealed class EditResourceCategoryEndpoint : EndpointHandlerBase<
    UpdateResourceCategoryApiRequest,
    UpdateResourceCategoryCommand,
    UpdateResourceCategoryCommandResponse,
    UpdateResourceCategoryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public EditResourceCategoryEndpoint(
        IPresentationMapper<UpdateResourceCategoryApiRequest, UpdateResourceCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Resource.EditResourceCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class UpdateResourceCategoryApiRequestMapper : IPresentationMapper<UpdateResourceCategoryApiRequest, UpdateResourceCategoryCommand>
{
    public ValueTask<PrimitiveResult<UpdateResourceCategoryCommand>> Map(UpdateResourceCategoryApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new UpdateResourceCategoryCommand(
                    src.Id,
                    src.Title,
                    src.Description,
                    src.ParentId)));
}