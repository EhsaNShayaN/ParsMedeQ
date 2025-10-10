using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.CartFeature.MergeCartFeature;

public sealed record class MergeCartCommand(
    int UserId,
    Guid AnonymousId) : IPrimitiveResultCommand<MergeCartCommandResponse>,
    IValidatableRequest<MergeCartCommand>
{
    public ValueTask<PrimitiveResult<MergeCartCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.AnonymousId)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}