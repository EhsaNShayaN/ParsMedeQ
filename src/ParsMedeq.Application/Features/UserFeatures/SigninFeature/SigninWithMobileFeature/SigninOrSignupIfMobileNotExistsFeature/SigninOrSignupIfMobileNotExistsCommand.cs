using ParsMedeQ.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninOrSignupIfMobileNotExistsFeature;
public sealed record SigninOrSignupIfMobileNotExistsCommand(string Mobile, string Otp) :
    IPrimitiveResultCommand<UserTokenInfo>,
    IValidatableRequest<SigninOrSignupIfMobileNotExistsCommand>
{
    public ValueTask<PrimitiveResult<SigninOrSignupIfMobileNotExistsCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
            .Ensure(_ => !string.IsNullOrWhiteSpace(this.Otp), PrimitiveError.Create("", "رمز یک بار مصرف ارسال نشده است"))
            .Map(_ => this);
    }
}
