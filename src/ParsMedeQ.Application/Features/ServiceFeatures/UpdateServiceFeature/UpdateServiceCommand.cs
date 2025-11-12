namespace ParsMedeQ.Application.Features.ServiceFeatures.UpdateServiceFeature;

public sealed record class UpdateServiceCommand(int Id, string Title, string Description, FileData? ImageInfo, string OldImagePath)
    : IPrimitiveResultCommand<UpdateServiceCommandResponse>,
    IValidatableRequest<UpdateServiceCommand>
{
    public ValueTask<PrimitiveResult<UpdateServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Id)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "TypeId ارسالی نامعتبر است"))
                ]);
}