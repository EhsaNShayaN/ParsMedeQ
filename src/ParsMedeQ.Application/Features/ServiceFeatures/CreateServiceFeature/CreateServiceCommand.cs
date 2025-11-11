namespace ParsMedeQ.Application.Features.ServiceFeatures.CreateServiceFeature;

public sealed record class CreateServiceCommand(byte TypeId, string LanguageCode, int ServiceId, string Title, string Description, FileData? ImageInfo)
    : IPrimitiveResultCommand<CreateServiceCommandResponse>, IValidatableRequest<CreateServiceCommand>
{
    public ValueTask<PrimitiveResult<CreateServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.TypeId)
                .Match(
                    _ => PrimitiveResult.Success(),
                    _ => PrimitiveResult.Failure("Validation.Error", "TypeId ارسالی نامعتبر است"))
                ]);
}
