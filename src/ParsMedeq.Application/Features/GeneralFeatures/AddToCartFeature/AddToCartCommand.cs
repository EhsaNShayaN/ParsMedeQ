using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.GeneralFeatures.AddToCartFeature;

public sealed record class AddToCartCommand(
    int? UserId,
    Guid? AnonymousId,
    CartItem CartItem) : IPrimitiveResultCommand<AddToCartCommandResponse>,
    IValidatableRequest<AddToCartCommand>
{
    public ValueTask<PrimitiveResult<AddToCartCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.AnonymousId)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}