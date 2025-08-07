using EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;
using EShop.Contracts;
using EShop.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.SendOtpContracts;

namespace EShop.Presentation.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;
internal sealed class SendOtpEndpoint : EndpointHandlerBase<
    SigninWithMobileSendOtpApiRequest,
    SigninWithMobileSendOtpCommand,
    SigninWithMobileSendOtpCommandResponse,
    SigninWithMobileSendOtpApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendOtpEndpoint() : base(
       EShopEndpoints.User.SigninWithMobile_SendOtp,
       HttpMethod.Post)
    {
    }
}
