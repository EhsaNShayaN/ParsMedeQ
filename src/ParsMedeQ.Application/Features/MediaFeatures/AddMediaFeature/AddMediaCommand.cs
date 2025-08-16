using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.MediaFeatures.AddMediaFeature;

public sealed record class AddMediaCommand(
    int TableId,
    string Path,
    string MimeType) : IPrimitiveResultCommand<AddMediaCommandResponse>,
    IValidatableRequest<AddMediaCommand>
{
    public ValueTask<PrimitiveResult<AddMediaCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Path)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}