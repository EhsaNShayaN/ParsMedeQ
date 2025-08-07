using EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpToExistingMobileFeature;
using EShop.Contracts;
using EShop.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.SendOtpToExistingMobileContracts;

namespace EShop.Presentation.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpToExistingMobileFeature;
internal sealed class SendOtpToExistingMobileEndpoint : EndpointHandlerBase<
    SendOtpToExistingMobileApiRequest,
    SendOtpToExistingMobileCommand,
    SendOtpToExistingMobileCommandResponse,
    SendOtpToExistingMobileApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendOtpToExistingMobileEndpoint() : base(
       EShopEndpoints.User.SendOtpToExistingMobile,
       HttpMethod.Post)
    {
    }
}
