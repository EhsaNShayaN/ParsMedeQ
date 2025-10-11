using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.CartFeature.AddToCartFeature;

public sealed record class AddToCartCommand(
    Guid AnonymousId,
    int RelatedId,
    int TableId,
    int Quantity) : IPrimitiveResultCommand<AddToCartCommandResponse>,
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