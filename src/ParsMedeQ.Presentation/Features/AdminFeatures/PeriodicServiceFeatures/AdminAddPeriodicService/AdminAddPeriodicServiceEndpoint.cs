using ParsMedeQ.Application.Features.ProductFeatures.AddPeriodicServiceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.AddPeriodicServiceContract;

namespace ParsMedeQ.Presentation.Features.AdminFeatures.PeriodicServiceFeatures.AdminAddPeriodicService;

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
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddPeriodicServiceCommand(src.Id, src.ProductId)
                ));
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