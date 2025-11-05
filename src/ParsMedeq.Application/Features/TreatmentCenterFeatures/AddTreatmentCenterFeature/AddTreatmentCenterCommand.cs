namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.AddTreatmentCenterFeature;

public sealed record class AddTreatmentCenterCommand(
    int LocationId,
    string Title,
    string Description,
    FileData ImageInfo) : IPrimitiveResultCommand<AddTreatmentCenterCommandResponse>, IValidatableRequest<AddTreatmentCenterCommand>
{
    public ValueTask<PrimitiveResult<AddTreatmentCenterCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}