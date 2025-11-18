using ParsMedeQ.Application.Features.OrderFeatures.AddPeriodicServiceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.OrderContracts.AddPeriodicServiceContract;
using ParsMedeQ.Domain.Helpers;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.OrderFeatures.AdminAddPeriodicService;

sealed class AdminAddPeriodicServiceEndpoint : EndpointHandlerBase<
    AddPeriodicServiceApiRequest,
    AddPeriodicServiceCommand,
    AddPeriodicServiceCommandResponse,
    AddPeriodicServiceApiResponse>
{
    protected override bool NeedTaxPayerFile => true;
    protected override bool NeedAdminPrivilage => true;
    protected override bool NeedAuthentication => true;

    public AdminAddPeriodicServiceEndpoint(
        IPresentationMapper<AddPeriodicServiceApiRequest, AddPeriodicServiceCommand> requestMapper,
        IPresentationMapper<AddPeriodicServiceCommandResponse, AddPeriodicServiceApiResponse> responseMapper)
        : base(
            Endpoints.Admin.AddPeriodicService,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class AddPeriodicServiceApiRequestMapper : IPresentationMapper<
    AddPeriodicServiceApiRequest,
    AddPeriodicServiceCommand>
{
    public ValueTask<PrimitiveResult<AddPeriodicServiceCommand>> Map(
        AddPeriodicServiceApiRequest src,
        CancellationToken cancellationToken)
    {
        var array = HashIdsHelper.HexSerializer.Deserialize(src.Id, v =>
        {
            return v.Split('|').Select(s => s.ToInt()).ToArray();
        });
        return ValueTask
            .FromResult(PrimitiveResult.Success(new AddPeriodicServiceCommand(
                array[0],
                array[1],
                array[2])));
    }
}
sealed class AddPeriodicServiceApiResponseMapper : IPresentationMapper<
    AddPeriodicServiceCommandResponse,
    AddPeriodicServiceApiResponse>
{
    public ValueTask<PrimitiveResult<AddPeriodicServiceApiResponse>> Map(
        AddPeriodicServiceCommandResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new AddPeriodicServiceApiResponse(src.Changed)));
    }
}