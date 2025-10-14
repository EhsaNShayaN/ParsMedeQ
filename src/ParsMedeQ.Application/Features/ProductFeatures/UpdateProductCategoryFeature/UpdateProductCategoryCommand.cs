using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.UpdateProductCategoryFeature;

public sealed record class UpdateProductCategoryCommand(
    int Id,
    string Title,
    string Description,
    int? ParentId,
    FileData? ImageInfo,
    string OldImagePath) : IPrimitiveResultCommand<UpdateProductCategoryCommandResponse>,
    IValidatableRequest<UpdateProductCategoryCommand>
{
    public ValueTask<PrimitiveResult<UpdateProductCategoryCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}