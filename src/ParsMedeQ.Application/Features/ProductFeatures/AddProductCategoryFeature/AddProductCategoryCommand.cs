using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductCategoryFeature;

public sealed record class AddProductCategoryCommand(
    int TableId,
    string Title,
    string Description,
    int? ParentId,
    byte[] Image,
    string ImageExtension) : IPrimitiveResultCommand<AddProductCategoryCommandResponse>,
    IValidatableRequest<AddProductCategoryCommand>
{
    public ValueTask<PrimitiveResult<AddProductCategoryCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}