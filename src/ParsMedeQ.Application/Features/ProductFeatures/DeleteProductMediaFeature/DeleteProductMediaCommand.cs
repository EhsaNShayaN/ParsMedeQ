using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.DeleteProductMediaFeature;

public sealed record class DeleteProductMediaCommand(
    int ProductId, int MediaId) : IPrimitiveResultCommand<DeleteProductMediaCommandResponse>,
    IValidatableRequest<DeleteProductMediaCommand>
{
    public ValueTask<PrimitiveResult<DeleteProductMediaCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.ProductId)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}