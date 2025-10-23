using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.PaymentFeatures.AddPaymentFeature;

public sealed record class AddPaymentCommand(
    int OrderId,
    decimal Amount) : IPrimitiveResultCommand<AddPaymentCommandResponse>,
    IValidatableRequest<AddPaymentCommand>
{
    public ValueTask<PrimitiveResult<AddPaymentCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(OrderId > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}