using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.SectionFeatures.AddSectionFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.AdminContracts.AdminAddSectionContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.SectionFeatures;
sealed class AdminAddSectionEndpoint : EndpointHandlerBase<
    AdminAddSectionApiRequest,
    AddSectionCommand,
    AddSectionCommandResponse,
    AdminAddSectionApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => true;

    public AdminAddSectionEndpoint(
        IPresentationMapper<AdminAddSectionApiRequest, AddSectionCommand> apiRequestMapper
        ) : base(
            Endpoints.Admin.AddSection,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AdminAddSectionApiRequestMapper : IPresentationMapper<AdminAddSectionApiRequest, AddSectionCommand>
{
    public IFileService _fileService { get; set; }

    public AdminAddSectionApiRequestMapper(IFileService fileService) => this._fileService = fileService;

    public async ValueTask<PrimitiveResult<AddSectionCommand>> Map(AdminAddSectionApiRequest src, CancellationToken cancellationToken)
    {
        var imageInfo = await _fileService.ReadStream(src.Image).ConfigureAwait(false);
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddSectionCommand(
                    src.Title,
                    src.Description,
                    imageInfo)));
    }
}