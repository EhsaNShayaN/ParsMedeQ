using ParsMedeQ.Application.Features.PaymentFeatures.FailPaymentFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.PaymentContracts.FailPaymentContract;

namespace ParsMedeQ.Presentation.Features.PaymentFeatures.FailPaymentFeature;
sealed class FailPaymentEndpoint : EndpointHandlerBase<
    FailPaymentApiRequest,
    FailPaymentCommand,
    FailPaymentCommandResponse,
    FailPaymentApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => false;

    public FailPaymentEndpoint(
        IPresentationMapper<FailPaymentApiRequest, FailPaymentCommand> apiRequestMapper
        ) : base(
            Endpoints.Payment.FailPayment,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class FailPaymentApiRequestMapper : IPresentationMapper<FailPaymentApiRequest, FailPaymentCommand>
{
    public async ValueTask<PrimitiveResult<FailPaymentCommand>> Map(FailPaymentApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new FailPaymentCommand(
                    src.PaymentId)));
    }
}