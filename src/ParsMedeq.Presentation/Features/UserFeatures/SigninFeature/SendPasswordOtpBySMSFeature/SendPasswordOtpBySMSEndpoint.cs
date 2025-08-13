using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SendPasswordOtpBySMSContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SendPasswordOtpBySMSFeature;
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
