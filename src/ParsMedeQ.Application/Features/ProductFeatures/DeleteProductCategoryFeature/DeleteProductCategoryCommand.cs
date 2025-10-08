using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.DeleteProductCategoryFeature;

public sealed record class DeleteProductCategoryCommand(int Id) :
    IPrimitiveResultCommand<DeleteProductCategoryCommandResponse>,
    IValidatableRequest<DeleteProductCategoryCommand>
{
    public ValueTask<PrimitiveResult<DeleteProductCategoryCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(Id > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}