using EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;
using EShop.Contracts;
using EShop.Contracts.UserContracts.SigninContracts.SendPasswordOtpBySMSContracts;

namespace EShop.Presentation.Features.EShopFeatures.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;
internal sealed class SendPasswordOtpBySMSEndpoint : EndpointHandlerBase<
    SendPasswordOtpBySMSApiRequest,
    SendPasswordOtpBySMSCommand,
    SendPasswordOtpBySMSCommandResponse,
    SendPasswordOtpBySMSApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendPasswordOtpBySMSEndpoint() : base(
       EShopEndpoints.User.SendPasswordOtpBySMS,
       HttpMethod.Post)
    {
    }
}
