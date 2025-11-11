namespace ParsMedeQ.Application.Features.ServiceFeatures.CreateServiceFeature;

public sealed record class CreateServiceCommand(
    byte TypeId) : IPrimitiveResultCommand<CreateServiceCommandResponse>, IValidatableRequest<CreateServiceCommand>
{
    public ValueTask<PrimitiveResult<CreateServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.TypeId)
                .Match(
                    _ => PrimitiveResult.Success(),
                    _ => PrimitiveResult.Failure("Validation.Error", "TypeId ارسالی نامعتبر است"))
                ]);
}
