using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.DeleteResourceCategoryFeature;

public sealed record class DeleteResourceCategoryCommand(int Id) :
    IPrimitiveResultCommand<DeleteResourceCategoryCommandResponse>,
    IValidatableRequest<DeleteResourceCategoryCommand>
{
    public ValueTask<PrimitiveResult<DeleteResourceCategoryCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(Id > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}