using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.DeleteProductFeature;

public sealed record class DeleteProductCommand(int Id) :
    IPrimitiveResultCommand<DeleteProductCommandResponse>,
    IValidatableRequest<DeleteProductCommand>
{
    public ValueTask<PrimitiveResult<DeleteProductCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(Id > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}