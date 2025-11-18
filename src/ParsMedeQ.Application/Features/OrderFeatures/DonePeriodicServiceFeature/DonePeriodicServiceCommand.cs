namespace ParsMedeQ.Application.Features.OrderFeatures.DonePeriodicServiceFeature;

public sealed record class DonePeriodicServiceCommand(
    int OrderId,
    int OrderItemId,
    int PeriodicServiceId) : IPrimitiveResultCommand<DonePeriodicServiceCommandResponse>,
    IValidatableRequest<DonePeriodicServiceCommand>
{
    public ValueTask<PrimitiveResult<DonePeriodicServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.PeriodicServiceId)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "شناسه ارسالی نامعتبر است"))
                ]);
}