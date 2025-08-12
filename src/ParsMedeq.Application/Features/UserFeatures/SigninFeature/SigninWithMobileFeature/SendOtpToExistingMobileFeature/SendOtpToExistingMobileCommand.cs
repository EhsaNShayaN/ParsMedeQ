using SRH.MediatRMessaging;

namespace ParsMedeq.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpToExistingMobileFeature;
public sealed record SendOtpToExistingMobileCommand(string Mobile) :
    IPrimitiveResultCommand<SendOtpToExistingMobileCommandResponse>,
    IValidatableRequest<SendOtpToExistingMobileCommand>
{
    public ValueTask<PrimitiveResult<SendOtpToExistingMobileCommand>> Validate()
    {
        return MobileType.Create(this.Mobile)
        .Map(_ => this);
    }
}
