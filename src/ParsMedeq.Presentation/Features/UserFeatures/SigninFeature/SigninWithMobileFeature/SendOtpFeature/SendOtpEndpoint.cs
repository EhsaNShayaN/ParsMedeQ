using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.SendOtpContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpFeature;
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
