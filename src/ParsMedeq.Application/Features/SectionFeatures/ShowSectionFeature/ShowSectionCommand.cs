namespace ParsMedeQ.Application.Features.SectionFeatures.ShowSectionFeature;

public sealed record class ShowSectionCommand(
    int Id) : IPrimitiveResultCommand<ShowSectionCommandResponse>,
    IValidatableRequest<ShowSectionCommand>
{
    public ValueTask<PrimitiveResult<ShowSectionCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Id)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}