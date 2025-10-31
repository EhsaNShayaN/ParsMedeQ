using ParsMedeQ.Application.Features.TreatmentCenterFeatures.DeleteTreatmentCenterFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TreatmentCenterContracts.DeleteTreatmentCenterContract;

namespace ParsMedeQ.Presentation.Features.TreatmentCenterFeatures.DeleteTreatmentCenterFeature;
sealed class DeleteTreatmentCenterEndpoint : EndpointHandlerBase<
    DeleteTreatmentCenterApiRequest,
    DeleteTreatmentCenterCommand,
    DeleteTreatmentCenterCommandResponse,
    DeleteTreatmentCenterApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public DeleteTreatmentCenterEndpoint(
        IPresentationMapper<DeleteTreatmentCenterApiRequest, DeleteTreatmentCenterCommand> apiRequestMapper
        ) : base(
            Endpoints.TreatmentCenter.DeleteTreatmentCenter,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class DeleteTreatmentCenterApiRequestMapper : IPresentationMapper<DeleteTreatmentCenterApiRequest, DeleteTreatmentCenterCommand>
{
    public ValueTask<PrimitiveResult<DeleteTreatmentCenterCommand>> Map(DeleteTreatmentCenterApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new DeleteTreatmentCenterCommand(src.Id)));
}