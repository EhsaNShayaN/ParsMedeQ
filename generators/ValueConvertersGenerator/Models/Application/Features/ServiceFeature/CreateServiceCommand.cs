using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.ServiceFeatures.CreateServiceFeature;

public sealed record class CreateServiceCommand() : IPrimitiveResultCommand<CreateServiceResponse>, IValidatableRequest<CreateService>
{
    public ValueTask<PrimitiveResult<CreateServiceCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}
