using ParsMedeQ.Application.Features.OrderFeatures.AddOrderFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.OrderContracts.AddOrderContract;

namespace ParsMedeQ.Presentation.Features.OrderFeatures.AddOrderFeature;
sealed class AddOrderEndpoint : EndpointHandlerBase<
    AddOrderApiRequest,
    AddOrderCommand,
    AddOrderCommandResponse,
    AddOrderApiResponse>
{
    protected override bool NeedAuthentication => true;
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
    private readonly IUserContextAccessor _userContextAccessor;

    public AddOrderApiRequestMapper(IUserContextAccessor userContextAccessor) => this._userContextAccessor = userContextAccessor;

    public async ValueTask<PrimitiveResult<AddOrderCommand>> Map(AddOrderApiRequest src, CancellationToken cancellationToken)
    {
        var validUserIds = new int[] { 1, 2, 8 };
        if (!validUserIds.Contains(this._userContextAccessor.GetCurrent().GetUserId() ?? 0)) throw new Exception("Invalid User");
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddOrderCommand(
                    src.CartId)));
    }
}