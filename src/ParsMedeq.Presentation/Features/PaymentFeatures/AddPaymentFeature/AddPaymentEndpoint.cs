using ParsMedeQ.Application.Features.PaymentFeatures.AddPaymentFeature;
using ParsMedeQ.Application.Features.PaymentFeatures.FailPaymentFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.PaymentContracts.AddPaymentContract;
using ParsMedeQ.Contracts.PaymentContracts.FailPaymentContract;

namespace ParsMedeQ.Presentation.Features.PaymentFeatures.AddPaymentFeature;
sealed class AddPaymentEndpoint : EndpointHandlerBase<
    AddPaymentApiRequest,
    AddPaymentCommand,
    AddPaymentCommandResponse,
    AddPaymentApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => false;

    public AddPaymentEndpoint(
        IPresentationMapper<AddPaymentApiRequest, AddPaymentCommand> apiRequestMapper
        ) : base(
            Endpoints.Payment.AddPayment,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddPaymentApiRequestMapper : IPresentationMapper<AddPaymentApiRequest, AddPaymentCommand>
{
    public async ValueTask<PrimitiveResult<AddPaymentCommand>> Map(AddPaymentApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddPaymentCommand(
                    src.OrderId,
                    src.Amount)));
    }
}