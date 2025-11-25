using ParsMedeQ.Application.Features.SectionFeatures.HideSectionFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.AdminContracts.AdminHideSectionContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.SectionFeatures;
sealed class AdminHideSectionEndpoint : EndpointHandlerBase<
    AdminHideSectionApiRequest,
    HideSectionCommand,
    HideSectionCommandResponse,
    AdminHideSectionApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => true;

    public AdminHideSectionEndpoint(
        IPresentationMapper<AdminHideSectionApiRequest, HideSectionCommand> apiRequestMapper
        ) : base(
            Endpoints.Admin.HideSection,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AdminHideSectionApiRequestMapper : IPresentationMapper<AdminHideSectionApiRequest, HideSectionCommand>
{
    public async ValueTask<PrimitiveResult<HideSectionCommand>> Map(AdminHideSectionApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(PrimitiveResult.Success(new HideSectionCommand(src.Id)));
    }
}