using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;
public sealed record SigninWithMobileSendOtpCommand(string Mobile) :
    IPrimitiveResultCommand<SigninWithMobileSendOtpCommandResponse>,
    IValidatableRequest<SigninWithMobileSendOtpCommand>
{
    public ValueTask<PrimitiveResult<SigninWithMobileSendOtpCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
        .Map(_ => this);
    }
}
