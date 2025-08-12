using ParsMedeq.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;
using ParsMedeq.Contracts;
using ParsMedeq.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.SendOtpContracts;

namespace ParsMedeq.Presentation.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;
internal sealed class SendOtpEndpoint : EndpointHandlerBase<
    SigninWithMobileSendOtpApiRequest,
    SigninWithMobileSendOtpCommand,
    SigninWithMobileSendOtpCommandResponse,
    SigninWithMobileSendOtpApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendOtpEndpoint() : base(
       Endpoints.User.SigninWithMobile_SendOtp,
       HttpMethod.Post)
    {
    }
}
