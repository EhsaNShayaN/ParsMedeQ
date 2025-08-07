using EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;
using EShop.Contracts;
using EShop.Contracts.UserContracts.SigninContracts.SendPasswordOtpByEmailContracts;

namespace EShop.Presentation.Features.EShopFeatures.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;
internal sealed class SendPasswordOtpByEmailEndpoint : EndpointHandlerBase<
    SendPasswordOtpByEmailApiRequest,
    SendPasswordOtpByEmailCommand,
    SendPasswordOtpByEmailCommandResponse,
    SendPasswordOtpByEmailApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendPasswordOtpByEmailEndpoint() : base(
       EShopEndpoints.User.SendPasswordOtpByEmail,
       HttpMethod.Post)
    {
    }
}
