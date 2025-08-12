using ParsMedeq.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;
using ParsMedeq.Contracts;
using ParsMedeq.Contracts.UserContracts.SigninContracts.SendPasswordOtpBySMSContracts;

namespace ParsMedeq.Presentation.Features.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;
internal sealed class SendPasswordOtpBySMSEndpoint : EndpointHandlerBase<
    SendPasswordOtpBySMSApiRequest,
    SendPasswordOtpBySMSCommand,
    SendPasswordOtpBySMSCommandResponse,
    SendPasswordOtpBySMSApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendPasswordOtpBySMSEndpoint() : base(
       Endpoints.User.SendPasswordOtpBySMS,
       HttpMethod.Post)
    {
    }
}
