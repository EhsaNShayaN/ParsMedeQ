namespace ParsMedeQ.Application.Features.SectionFeatures.UpdateSectionFeature;

public sealed record class UpdateSectionCommand(
    int Id,
    string Title,
    string Description,
    FileData? ImageInfo,
    string OldImagePath) : IPrimitiveResultCommand<UpdateSectionCommandResponse>,
    IValidatableRequest<UpdateSectionCommand>
{
    public ValueTask<PrimitiveResult<UpdateSectionCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}