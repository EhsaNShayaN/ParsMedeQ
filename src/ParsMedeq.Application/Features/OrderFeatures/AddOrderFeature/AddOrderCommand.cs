using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.OrderFeatures.AddOrderFeature;

public sealed record class AddOrderCommand(
    int CartId) : IPrimitiveResultCommand<AddOrderCommandResponse>,
    IValidatableRequest<AddOrderCommand>
{
    public ValueTask<PrimitiveResult<AddOrderCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(CartId > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}