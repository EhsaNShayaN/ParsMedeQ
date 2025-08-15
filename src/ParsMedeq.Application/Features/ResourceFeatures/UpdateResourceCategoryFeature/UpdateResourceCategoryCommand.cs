using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceCategoryFeature;

public sealed record class UpdateResourceCategoryCommand(
    int Id,
    string Title,
    string Description,
    int? ParentId) : IPrimitiveResultCommand<UpdateResourceCategoryCommandResponse>,
    IValidatableRequest<UpdateResourceCategoryCommand>
{
    public ValueTask<PrimitiveResult<UpdateResourceCategoryCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}