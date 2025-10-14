using ParsMedeQ.Application.Features.CartFeature.GetCartFeature;
using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.CartFeature.RemoveFromCartFeature;

public sealed record class RemoveFromCartCommand(
    Guid? AnonymousId,
    int RelatedId) : IPrimitiveResultCommand<CartListQueryResponse>,
    IValidatableRequest<RemoveFromCartCommand>
{
    public ValueTask<PrimitiveResult<RemoveFromCartCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.AnonymousId)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}