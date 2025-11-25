namespace ParsMedeQ.Application.Features.SectionFeatures.UpdateSectionByListFeature;

public sealed record class UpdateSectionByListCommand(
    int Id,
    UpdateSectionByListItemCommand[] Items) : IPrimitiveResultCommand<UpdateSectionByListCommandResponse>,
    IValidatableRequest<UpdateSectionByListCommand>
{
    public ValueTask<PrimitiveResult<UpdateSectionByListCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Id)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}
public sealed record class UpdateSectionByListItemCommand(
    string Title,
    string Description);
