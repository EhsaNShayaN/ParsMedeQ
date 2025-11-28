using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithPasswordFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithPasswordFeature;

internal sealed class SigninWithPasswordEndpoint : EndpointHandlerBase<
    SigninWithPasswordApiRequest,
    SigninWithPasswordCommand,
    UserTokenInfo,
    SigninWithPasswordApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SigninWithPasswordEndpoint(
        IPresentationMapper<SigninWithPasswordApiRequest, SigninWithPasswordCommand> requestMapper,
        IPresentationMapper<UserTokenInfo, SigninWithPasswordApiResponse> responseMapper) : base(
            Endpoints.User.SigninWithPassword,
            HttpMethod.Post,
            requestMapper,
            responseMapper)
    {
    }
}
sealed class SigninMobileExistsApiRequestMApper : IPresentationMapper<SigninWithPasswordApiRequest, SigninWithPasswordCommand>
{
    public ValueTask<PrimitiveResult<SigninWithPasswordCommand>> Map(SigninWithPasswordApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new SigninWithPasswordCommand(src.Mobile, src.Password)));
}


sealed class SigninMobileExistsApiResponseMapper : IPresentationMapper<UserTokenInfo, SigninWithPasswordApiResponse>
{
    public ValueTask<PrimitiveResult<SigninWithPasswordApiResponse>> Map(UserTokenInfo src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new SigninWithPasswordApiResponse(
                    src.Token,
                    src.Fullname,
                    src.Mobile)));
}

