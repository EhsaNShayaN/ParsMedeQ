using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.SectionFeatures.UpdateSectionFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.AdminContracts.AdminUpdateSectionContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.SectionFeatures;
sealed class AdminUpdateSectionEndpoint : EndpointHandlerBase<
    AdminUpdateSectionApiRequest,
    UpdateSectionCommand,
    UpdateSectionCommandResponse,
    AdminUpdateSectionApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => true;

    public AdminUpdateSectionEndpoint(
        IPresentationMapper<AdminUpdateSectionApiRequest, UpdateSectionCommand> apiRequestMapper
        ) : base(
            Endpoints.Admin.UpdateSection,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AdminUpdateSectionApiRequestMapper : IPresentationMapper<AdminUpdateSectionApiRequest, UpdateSectionCommand>
{
    public IFileService _fileService { get; set; }

    public AdminUpdateSectionApiRequestMapper(IFileService fileService) => this._fileService = fileService;

    public async ValueTask<PrimitiveResult<UpdateSectionCommand>> Map(AdminUpdateSectionApiRequest src, CancellationToken cancellationToken)
    {
        var imageInfo = await _fileService.ReadStream(src.Image).ConfigureAwait(false);
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new UpdateSectionCommand(
                    src.Id,
                    src.Title,
                    src.Description,
                    imageInfo,
                    src.OldImagePath)));
    }
}