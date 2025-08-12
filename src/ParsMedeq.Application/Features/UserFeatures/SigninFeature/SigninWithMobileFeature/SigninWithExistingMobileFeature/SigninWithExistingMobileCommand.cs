using ParsMedeq.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging;

namespace ParsMedeq.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithExistingMobileFeature;
public sealed record SigninWithExistingMobileCommand(string Mobile, string Otp) :
    IPrimitiveResultCommand<UserTokenInfo>,
    IValidatableRequest<SigninWithExistingMobileCommand>
{
    public ValueTask<PrimitiveResult<SigninWithExistingMobileCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
            .Ensure(_ => !string.IsNullOrWhiteSpace(this.Otp), PrimitiveError.Create("", "رمز یک بار مصرف ارسال نشده است"))
            .Map(_ => this);
    }
}
