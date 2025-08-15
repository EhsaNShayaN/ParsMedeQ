using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceCategoryFeature;

public sealed record class AddResourceCategoryCommand(
    int TableId,
    string Title,
    string Description,
    int? ParentId) : IPrimitiveResultCommand<AddResourceCategoryCommandResponse>,
    IValidatableRequest<AddResourceCategoryCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCategoryCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}