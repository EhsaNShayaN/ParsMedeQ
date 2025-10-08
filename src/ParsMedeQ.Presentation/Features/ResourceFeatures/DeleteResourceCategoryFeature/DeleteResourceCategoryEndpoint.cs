using ParsMedeQ.Application.Features.ResourceFeatures.DeleteResourceCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.DeleteResourceCategoryContract;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.DeleteResourceCategoryFeature;
sealed class DeleteResourceCategoryEndpoint : EndpointHandlerBase<
    DeleteResourceCategoryApiRequest,
    DeleteResourceCategoryCommand,
    DeleteResourceCategoryCommandResponse,
    DeleteResourceCategoryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public DeleteResourceCategoryEndpoint(
        IPresentationMapper<DeleteResourceCategoryApiRequest, DeleteResourceCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Resource.DeleteResourceCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class DeleteResourceCategoryApiRequestMapper : IPresentationMapper<DeleteResourceCategoryApiRequest, DeleteResourceCategoryCommand>
{
    public ValueTask<PrimitiveResult<DeleteResourceCategoryCommand>> Map(DeleteResourceCategoryApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new DeleteResourceCategoryCommand(src.Id)));
}