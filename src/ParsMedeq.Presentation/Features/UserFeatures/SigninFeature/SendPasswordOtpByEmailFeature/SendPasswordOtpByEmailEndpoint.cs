using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SendPasswordOtpByEmailContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;
internal sealed class SendPasswordOtpByEmailEndpoint : EndpointHandlerBase<
    SendPasswordOtpByEmailApiRequest,
    SendPasswordOtpByEmailCommand,
    SendPasswordOtpByEmailCommandResponse,
    SendPasswordOtpByEmailApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendPasswordOtpByEmailEndpoint() : base(
       Endpoints.User.SendPasswordOtpByEmail,
       HttpMethod.Post)
    {
    }
}
