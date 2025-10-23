using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.PaymentFeatures.FailPaymentFeature;

public sealed record class FailPaymentCommand(
    int PaymentId) : IPrimitiveResultCommand<FailPaymentCommandResponse>,
    IValidatableRequest<FailPaymentCommand>
{
    public ValueTask<PrimitiveResult<FailPaymentCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(PaymentId > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}