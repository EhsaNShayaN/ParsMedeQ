using ParsMedeQ.Application.Features.SectionFeatures.UpdateSectionByListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.AdminContracts.AdminUpdateSectionByListContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.SectionFeatures;
sealed class AdminUpdateSectionByListEndpoint : EndpointHandlerBase<
    AdminUpdateSectionByListApiRequest,
    UpdateSectionByListCommand,
    UpdateSectionByListCommandResponse,
    AdminUpdateSectionByListApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => true;

    public AdminUpdateSectionByListEndpoint(
        IPresentationMapper<AdminUpdateSectionByListApiRequest, UpdateSectionByListCommand> apiRequestMapper
        ) : base(
            Endpoints.Admin.UpdateSectionByList,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AdminUpdateSectionByListApiRequestMapper : IPresentationMapper<
    AdminUpdateSectionByListApiRequest,
    UpdateSectionByListCommand>
{
    public async ValueTask<PrimitiveResult<UpdateSectionByListCommand>> Map(
        AdminUpdateSectionByListApiRequest src,
        CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new UpdateSectionByListCommand(
                    src.Id,
                    src.Items.Select(s => new UpdateSectionByListItemCommand(s.Title, s.Description, s.Image)).ToArray())));
    }
}