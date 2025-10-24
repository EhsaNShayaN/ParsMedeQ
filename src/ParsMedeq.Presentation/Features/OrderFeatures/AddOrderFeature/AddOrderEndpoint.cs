using ParsMedeQ.Application.Features.OrderFeatures.AddOrderFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.OrderContracts.AddOrderContract;
using ParsMedeQ.Contracts.OrderContracts.OrderDetailsContract;

namespace ParsMedeQ.Presentation.Features.OrderFeatures.AddOrderFeature;
sealed class AddOrderEndpoint : EndpointHandlerBase<
    AddOrderApiRequest,
    AddOrderCommand,
    AddOrderCommandResponse,
    AddOrderApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedAdminPrivilage => true;
    protected override bool NeedTaxPayerFile => false;

    public AddOrderEndpoint(
        IPresentationMapper<AddOrderApiRequest, AddOrderCommand> apiRequestMapper
        ) : base(
            Endpoints.Order.AddOrder,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddOrderApiRequestMapper : IPresentationMapper<AddOrderApiRequest, AddOrderCommand>
{
    public async ValueTask<PrimitiveResult<AddOrderCommand>> Map(AddOrderApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddOrderCommand(
                    src.CartId)));
    }
}