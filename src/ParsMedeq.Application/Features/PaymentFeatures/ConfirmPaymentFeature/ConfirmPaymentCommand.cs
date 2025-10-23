using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.PaymentFeatures.ConfirmPaymentFeature;

public sealed record class ConfirmPaymentCommand(
    int PaymentId,
    string TransactionId) : IPrimitiveResultCommand<ConfirmPaymentCommandResponse>,
    IValidatableRequest<ConfirmPaymentCommand>
{
    public ValueTask<PrimitiveResult<ConfirmPaymentCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(PaymentId > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}