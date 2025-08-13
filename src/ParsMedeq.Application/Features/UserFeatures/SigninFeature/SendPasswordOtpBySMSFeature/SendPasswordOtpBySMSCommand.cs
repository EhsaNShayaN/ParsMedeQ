using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;
public sealed record SendPasswordOtpBySMSCommand(string Mobile) :
    IPrimitiveResultCommand<SendPasswordOtpBySMSCommandResponse>,
    IValidatableRequest<SendPasswordOtpBySMSCommand>
{
    public ValueTask<PrimitiveResult<SendPasswordOtpBySMSCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
        .Map(_ => this);
    }
}
