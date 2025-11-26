using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.CheckSiginFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.CheckSiginContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.CheckSiginFeature;
internal sealed class CheckSiginEndpoint : EndpointHandlerBase<
    CheckSiginApiRequest,
    CheckSiginCommand,
    CheckSiginCommandResponse,
    CheckSiginApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public CheckSiginEndpoint() : base(
       Endpoints.User.SigninWithMobile_CheckSigin,
       HttpMethod.Post)
    {
    }
}
