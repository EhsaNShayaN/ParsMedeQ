using SRH.MediatRMessaging;

namespace EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpToExistingMobileFeature;
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
