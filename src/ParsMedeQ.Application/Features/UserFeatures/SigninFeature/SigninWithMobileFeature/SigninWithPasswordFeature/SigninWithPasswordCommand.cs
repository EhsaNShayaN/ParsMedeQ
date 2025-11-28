using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithPasswordFeature;
public sealed record SigninWithPasswordCommand(string Mobile, string Password) :
    IPrimitiveResultCommand<UserTokenInfo>,
    IValidatableRequest<SigninWithPasswordCommand>
{
    public ValueTask<PrimitiveResult<SigninWithPasswordCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
            .Ensure(_ => !string.IsNullOrWhiteSpace(this.Password),
            PrimitiveError.Create("", "رمز ورود ارسال نشده است"))
            .Map(_ => this);
    }
}
