using ParsMedeQ.Application.Features.PaymentFeatures.ConfirmPaymentFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.PaymentContracts.ConfirmPaymentContract;

namespace ParsMedeQ.Presentation.Features.PaymentFeatures.ConfirmPaymentFeature;
sealed class ConfirmPaymentEndpoint : EndpointHandlerBase<
    ConfirmPaymentApiRequest,
    ConfirmPaymentCommand,
    ConfirmPaymentCommandResponse,
    ConfirmPaymentApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => false;

    public ConfirmPaymentEndpoint(
        IPresentationMapper<ConfirmPaymentApiRequest, ConfirmPaymentCommand> apiRequestMapper
        ) : base(
            Endpoints.Payment.ConfirmPayment,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class ConfirmPaymentApiRequestMapper : IPresentationMapper<ConfirmPaymentApiRequest, ConfirmPaymentCommand>
{
    private readonly IUserContextAccessor _userContextAccessor;

    public ConfirmPaymentApiRequestMapper(IUserContextAccessor userContextAccessor) => this._userContextAccessor = userContextAccessor;

    public async ValueTask<PrimitiveResult<ConfirmPaymentCommand>> Map(ConfirmPaymentApiRequest src, CancellationToken cancellationToken)
    {
        var validUserIds = new int[] { 1, 2, 8 };
        if (!validUserIds.Contains(this._userContextAccessor.GetCurrent().GetUserId() ?? 0)) throw new Exception("Invalid User");
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new ConfirmPaymentCommand(
                    src.PaymentId,
                    src.TransactionId)));
    }
}