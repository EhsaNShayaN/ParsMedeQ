using ParsMedeQ.Application.Features.ProductFeatures.DonePeriodicServiceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.DonePeriodicServiceContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.PeriodicServiceFeatures.AdminDonePeriodicService;

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
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new DonePeriodicServiceCommand(
                    src.Id,
                    src.ProductId)
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