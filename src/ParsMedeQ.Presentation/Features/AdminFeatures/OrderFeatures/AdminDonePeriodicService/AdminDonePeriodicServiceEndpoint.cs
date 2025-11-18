using ParsMedeQ.Application.Features.OrderFeatures.DonePeriodicServiceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.OrderContracts.DonePeriodicServiceContract;
using ParsMedeQ.Domain.Helpers;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.OrderFeatures.AdminDonePeriodicService;

sealed class AdminDonePeriodicServiceEndpoint : EndpointHandlerBase<
    DonePeriodicServiceApiRequest,
    DonePeriodicServiceCommand,
    DonePeriodicServiceCommandResponse,
    DonePeriodicServiceApiResponse>
{
    protected override bool NeedTaxPayerFile => true;
    protected override bool NeedAdminPrivilage => true;
    protected override bool NeedAuthentication => true;

    public AdminDonePeriodicServiceEndpoint(
        IPresentationMapper<DonePeriodicServiceApiRequest, DonePeriodicServiceCommand> requestMapper,
        IPresentationMapper<DonePeriodicServiceCommandResponse, DonePeriodicServiceApiResponse> responseMapper)
        : base(
            Endpoints.Admin.DonePeriodicService,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class DonePeriodicServiceApiRequestMapper : IPresentationMapper<
    DonePeriodicServiceApiRequest,
    DonePeriodicServiceCommand>
{
    public ValueTask<PrimitiveResult<DonePeriodicServiceCommand>> Map(
        DonePeriodicServiceApiRequest src,
        CancellationToken cancellationToken)
    {
        var array = HashIdsHelper.HexSerializer.Deserialize(src.Id, v =>
        {
            return v.Split('|').Select(s => s.ToInt()).ToArray();
        });
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new DonePeriodicServiceCommand(
                array[0],
                array[1],
                array[2])
                ));
    }
}
sealed class DonePeriodicServiceApiResponseMapper : IPresentationMapper<
    DonePeriodicServiceCommandResponse,
    DonePeriodicServiceApiResponse>
{
    public ValueTask<PrimitiveResult<DonePeriodicServiceApiResponse>> Map(
        DonePeriodicServiceCommandResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new DonePeriodicServiceApiResponse(src.Changed)));
    }
}