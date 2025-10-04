using ParsMedeQ.Application.Features.ResourceFeatures.DeleteResourceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.DeleteResourceContract;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.DeleteResourceFeature;
sealed class DeleteResourceEndpoint : EndpointHandlerBase<
    DeleteResourceApiRequest,
    DeleteResourceCommand,
    DeleteResourceCommandResponse,
    DeleteResourceApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public DeleteResourceEndpoint(
        IPresentationMapper<DeleteResourceApiRequest, DeleteResourceCommand> apiRequestMapper
        ) : base(
            Endpoints.Resource.DeleteResource,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class DeleteResourceApiRequestMapper : IPresentationMapper<DeleteResourceApiRequest, DeleteResourceCommand>
{
    public ValueTask<PrimitiveResult<DeleteResourceCommand>> Map(DeleteResourceApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new DeleteResourceCommand(src.Id)));
}