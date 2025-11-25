using ParsMedeQ.Application.Features.SectionFeatures.ShowSectionFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.AdminContracts.AdminShowSectionContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.SectionFeatures;
sealed class AdminShowSectionEndpoint : EndpointHandlerBase<
    AdminShowSectionApiRequest,
    ShowSectionCommand,
    ShowSectionCommandResponse,
    AdminShowSectionApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => true;

    public AdminShowSectionEndpoint(
        IPresentationMapper<AdminShowSectionApiRequest, ShowSectionCommand> apiRequestMapper
        ) : base(
            Endpoints.Admin.ShowSection,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AdminShowSectionApiRequestMapper : IPresentationMapper<AdminShowSectionApiRequest, ShowSectionCommand>
{
    public async ValueTask<PrimitiveResult<ShowSectionCommand>> Map(AdminShowSectionApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(PrimitiveResult.Success(new ShowSectionCommand(src.Id)));
    }
}