using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ResourceFeatures.DeleteResourceFeature;

public sealed record class DeleteResourceCommand(int Id) : 
    IPrimitiveResultCommand<DeleteResourceCommandResponse>,
    IValidatableRequest<DeleteResourceCommand>
{
    public ValueTask<PrimitiveResult<DeleteResourceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(Id > 0)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "عشق ارسالی نامعتبر است"))
                ]);
}