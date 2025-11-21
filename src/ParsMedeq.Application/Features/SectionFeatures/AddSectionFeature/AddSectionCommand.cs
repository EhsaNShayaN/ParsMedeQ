namespace ParsMedeQ.Application.Features.SectionFeatures.AddSectionFeature;

public sealed record class AddSectionCommand(
    string Title,
    string Description,
    FileData? ImageInfo) : IPrimitiveResultCommand<AddSectionCommandResponse>,
    IValidatableRequest<AddSectionCommand>
{
    public ValueTask<PrimitiveResult<AddSectionCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}