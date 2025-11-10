namespace ParsMedeQ.Application.Features.ProductFeatures.AddPeriodicServiceFeature;

public sealed record class AddPeriodicServiceCommand(
    int Id,
    int ProductId) : IPrimitiveResultCommand<AddPeriodicServiceCommandResponse>,
    IValidatableRequest<AddPeriodicServiceCommand>
{
    public ValueTask<PrimitiveResult<AddPeriodicServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Id)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}