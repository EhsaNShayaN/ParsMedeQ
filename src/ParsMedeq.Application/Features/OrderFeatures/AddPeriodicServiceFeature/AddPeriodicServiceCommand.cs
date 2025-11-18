namespace ParsMedeQ.Application.Features.OrderFeatures.AddPeriodicServiceFeature;

public sealed record class AddPeriodicServiceCommand(
    int OrderId,
    int OrderItemId,
    int PeriodicServiceId) : IPrimitiveResultCommand<AddPeriodicServiceCommandResponse>,
    IValidatableRequest<AddPeriodicServiceCommand>
{
    public ValueTask<PrimitiveResult<AddPeriodicServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.PeriodicServiceId)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}