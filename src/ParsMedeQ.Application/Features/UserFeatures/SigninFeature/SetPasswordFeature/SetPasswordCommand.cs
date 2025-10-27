using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SetPasswordFeature;
public sealed record class SetPasswordCommand(string Password) : IPrimitiveResultCommand<SetPasswordCommandResponse>,
    IValidatableRequest<SetPasswordCommand>
{
    public ValueTask<PrimitiveResult<SetPasswordCommand>> Validate()
    {
        return PrimitiveResult.Success(this)
            .Ensure([
                value => string.IsNullOrWhiteSpace(value.Password)
                        ? ValueTask.FromResult(PrimitiveResult.Failure("Validation.Error", "رمز عبور ارسال نشده است"))
                        : ValueTask.FromResult(PrimitiveResult.Success())
            ]);
    }
}