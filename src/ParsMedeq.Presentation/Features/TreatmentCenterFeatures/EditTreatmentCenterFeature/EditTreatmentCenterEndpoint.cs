using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.TreatmentCenterFeatures.UpdateTreatmentCenterFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TreatmentCenterContracts.UpdateTreatmentCenterContract;

namespace ParsMedeQ.Presentation.Features.TreatmentCenterFeatures.EditTreatmentCenterFeature;
sealed class EditTreatmentCenterEndpoint : EndpointHandlerBase<
    UpdateTreatmentCenterApiRequest,
    UpdateTreatmentCenterCommand,
    UpdateTreatmentCenterCommandResponse,
    UpdateTreatmentCenterApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public EditTreatmentCenterEndpoint(
        IPresentationMapper<UpdateTreatmentCenterApiRequest, UpdateTreatmentCenterCommand> apiRequestMapper) : base(
            Endpoints.TreatmentCenter.EditTreatmentCenter,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class UpdateTreatmentCenterApiRequestMapper : IPresentationMapper<UpdateTreatmentCenterApiRequest, UpdateTreatmentCenterCommand>
{
    public IFileService _fileService { get; set; }

    public UpdateTreatmentCenterApiRequestMapper(IFileService fileService) => this._fileService = fileService;

    public async ValueTask<PrimitiveResult<UpdateTreatmentCenterCommand>> Map(UpdateTreatmentCenterApiRequest src, CancellationToken cancellationToken)
    {
        var imageInfo = await _fileService.ReadStream(src.Image).ConfigureAwait(false);
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new UpdateTreatmentCenterCommand(
                    src.Id,
                    src.CityId,
                    src.Title,
                    src.Description,
                    imageInfo,
                    src.OldImagePath)));
    }
}