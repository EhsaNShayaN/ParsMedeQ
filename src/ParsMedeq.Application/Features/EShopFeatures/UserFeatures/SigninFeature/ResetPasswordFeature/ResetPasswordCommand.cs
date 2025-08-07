using EShop.Domain.Types.Mobile;
using SRH.MediatRMessaging;

namespace EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.ResetPasswordFeature;
public sealed record class ResetPasswordCommand(
    string Mobile,
    string Otp,
    string Password) : IPrimitiveResultCommand<ResetPasswordCommandResponse>,
    IValidatableRequest<ResetPasswordCommand>
{
    public ValueTask<PrimitiveResult<ResetPasswordCommand>> Validate()
    {
        return PrimitiveResult.Success(this)
            .Ensure([
                value => string.IsNullOrWhiteSpace(value.Password)
                        ? ValueTask.FromResult(PrimitiveResult.Failure("Validation.Error", "رمز عبور ارسال نشده است"))
                        : ValueTask.FromResult(PrimitiveResult.Success()),
                
                value => string.IsNullOrWhiteSpace(value.Otp)
                        ? ValueTask.FromResult(PrimitiveResult.Failure("Validation.Error", "رمز یکبار مصرف ارسال نشده است"))
                        : ValueTask.FromResult(PrimitiveResult.Success()),
                
                value => MobileType.Create(value.Mobile)
                    .Match(
                        _ => PrimitiveResult.Success() , 
                        _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
            ]);
    }
}