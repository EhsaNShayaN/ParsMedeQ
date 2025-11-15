using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.TreatmentCenterFeatures.AddTreatmentCenterFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.TreatmentCenterContracts.AddTreatmentCenterContract;

namespace ParsMedeQ.Presentation.Features.TreatmentCenterFeatures.AddTreatmentCenterFeature;
sealed class AddTreatmentCenterEndpoint : EndpointHandlerBase<
    AddTreatmentCenterApiRequest,
    AddTreatmentCenterCommand,
    AddTreatmentCenterCommandResponse,
    AddTreatmentCenterApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddTreatmentCenterEndpoint(
        IPresentationMapper<AddTreatmentCenterApiRequest, AddTreatmentCenterCommand> apiRequestMapper
        ) : base(
            Endpoints.TreatmentCenter.AddTreatmentCenter,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddTreatmentCenterApiRequestMapper : IPresentationMapper<AddTreatmentCenterApiRequest, AddTreatmentCenterCommand>
{
    public IFileService _fileService { get; set; }

    public AddTreatmentCenterApiRequestMapper(IFileService fileService) => this._fileService = fileService;

    public async ValueTask<PrimitiveResult<AddTreatmentCenterCommand>> Map(AddTreatmentCenterApiRequest src, CancellationToken cancellationToken)
    {
        var imageInfo = await _fileService.ReadStream(src.Image).ConfigureAwait(false);
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddTreatmentCenterCommand(
                    src.ProvinceId,
                    src.CityId,
                    src.Title,
                    src.Description,
                    imageInfo.Value)));
    }
}