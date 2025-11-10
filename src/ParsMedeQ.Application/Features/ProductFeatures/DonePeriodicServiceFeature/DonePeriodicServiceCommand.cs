namespace ParsMedeQ.Application.Features.ProductFeatures.DonePeriodicServiceFeature;

public sealed record class DonePeriodicServiceCommand(
    int Id,
    int ProductId) : IPrimitiveResultCommand<DonePeriodicServiceCommandResponse>,
    IValidatableRequest<DonePeriodicServiceCommand>
{
    public ValueTask<PrimitiveResult<DonePeriodicServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Id)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}