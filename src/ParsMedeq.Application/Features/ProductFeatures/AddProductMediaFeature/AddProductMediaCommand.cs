using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductMediaFeature;

public sealed record class AddProductMediaCommand(
    int ProductId,
    byte[][] FilesArray,
    string[] FileExtensions) : IPrimitiveResultCommand<AddProductMediaCommandResponse>,
    IValidatableRequest<AddProductMediaCommand>
{
    public ValueTask<PrimitiveResult<AddProductMediaCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.FilesArray)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}