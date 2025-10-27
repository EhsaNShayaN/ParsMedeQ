using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.ChangePasswordFeature;
public sealed record class ChangePasswordCommand(
    string Otp,
    string Password) : IPrimitiveResultCommand<ChangePasswordCommandResponse>,
    IValidatableRequest<ChangePasswordCommand>
{
    public ValueTask<PrimitiveResult<ChangePasswordCommand>> Validate()
    {
        return PrimitiveResult.Success(this)
            .Ensure([
                value => string.IsNullOrWhiteSpace(value.Password)
                        ? ValueTask.FromResult(PrimitiveResult.Failure("Validation.Error", "رمز عبور ارسال نشده است"))
                        : ValueTask.FromResult(PrimitiveResult.Success()),

                value => string.IsNullOrWhiteSpace(value.Otp)
                        ? ValueTask.FromResult(PrimitiveResult.Failure("Validation.Error", "رمز یکبار مصرف ارسال نشده است"))
                        : ValueTask.FromResult(PrimitiveResult.Success())
            ]);
    }
}