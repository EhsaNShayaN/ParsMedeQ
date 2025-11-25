namespace ParsMedeQ.Application.Features.SectionFeatures.HideSectionFeature;

public sealed record class HideSectionCommand(
    int Id) : IPrimitiveResultCommand<HideSectionCommandResponse>,
    IValidatableRequest<HideSectionCommand>
{
    public ValueTask<PrimitiveResult<HideSectionCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Id)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}