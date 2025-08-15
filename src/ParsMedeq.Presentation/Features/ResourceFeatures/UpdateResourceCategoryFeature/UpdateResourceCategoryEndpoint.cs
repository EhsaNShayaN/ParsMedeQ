using ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.UpdateResourceCategoryContract;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.UpdateResourceCategoryFeature;
sealed class UpdateResourceCategoryEndpoint : EndpointHandlerBase<
    UpdateResourceCategoryApiRequest,
    UpdateResourceCategoryCommand,
    UpdateResourceCategoryCommandResponse,
    UpdateResourceCategoryApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public UpdateResourceCategoryEndpoint(
        IPresentationMapper<UpdateResourceCategoryApiRequest, UpdateResourceCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Resource.UpdateResourceCategory,
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