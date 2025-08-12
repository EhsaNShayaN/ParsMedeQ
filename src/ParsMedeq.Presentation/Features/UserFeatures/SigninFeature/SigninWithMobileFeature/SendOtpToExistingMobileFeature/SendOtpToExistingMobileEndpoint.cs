using ParsMedeq.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpToExistingMobileFeature;
using ParsMedeq.Contracts;
using ParsMedeq.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.SendOtpToExistingMobileContracts;

namespace ParsMedeq.Presentation.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SendOtpToExistingMobileFeature;
internal sealed class SendOtpToExistingMobileEndpoint : EndpointHandlerBase<
    SendOtpToExistingMobileApiRequest,
    SendOtpToExistingMobileCommand,
    SendOtpToExistingMobileCommandResponse,
    SendOtpToExistingMobileApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SendOtpToExistingMobileEndpoint() : base(
       Endpoints.User.SendOtpToExistingMobile,
       HttpMethod.Post)
    {
    }
}
