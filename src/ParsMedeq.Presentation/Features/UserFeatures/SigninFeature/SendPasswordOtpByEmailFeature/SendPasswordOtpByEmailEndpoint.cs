using ParsMedeq.Application.Features.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;
using ParsMedeq.Contracts;
using ParsMedeq.Contracts.UserContracts.SigninContracts.SendPasswordOtpByEmailContracts;

namespace ParsMedeq.Presentation.Features.UserFeatures.SigninFeature.SendPasswordOtpByEmailFeature;
internal sealed class SendPasswordOtpByEmailEndpoint : EndpointHandlerBase<
    SendPasswordOtpByEmailApiRequest,
    SendPasswordOtpByEmailCommand,
    SendPasswordOtpByEmailCommandResponse,
    SendPasswordOtpByEmailApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendPasswordOtpByEmailEndpoint() : base(
       Endpoints.User.SendPasswordOtpByEmail,
       HttpMethod.Post)
    {
    }
}
