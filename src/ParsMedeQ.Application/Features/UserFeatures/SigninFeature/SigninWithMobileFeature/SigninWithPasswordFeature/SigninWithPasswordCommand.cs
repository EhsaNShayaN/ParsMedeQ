using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithPasswordFeature;
public sealed record SigninWithPasswordCommand(string Mobile, string Passwrod) :
    IPrimitiveResultCommand<UserTokenInfo>,
    IValidatableRequest<SigninWithPasswordCommand>
{
    public ValueTask<PrimitiveResult<SigninWithPasswordCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
            .Ensure(_ => !string.IsNullOrWhiteSpace(this.Passwrod),
            PrimitiveError.Create("", "رمز ورود ارسال نشده است"))
            .Map(_ => this);
    }
}
