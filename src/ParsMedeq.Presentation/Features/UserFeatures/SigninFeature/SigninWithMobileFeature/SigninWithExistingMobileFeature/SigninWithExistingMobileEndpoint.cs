using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithExistingMobileFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithExistingMobileFeature;

internal sealed class SigninWithExistingMobileEndpoint : EndpointHandlerBase<
    SigninWithExistingMobileApiRequest,
    SigninWithExistingMobileCommand,
    UserTokenInfo,
    SigninWithExistingMobileApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SigninWithExistingMobileEndpoint(
        IPresentationMapper<SigninWithExistingMobileApiRequest, SigninWithExistingMobileCommand> requestMapper,
        IPresentationMapper<UserTokenInfo, SigninWithExistingMobileApiResponse> responseMapper) : base(
            Endpoints.User.SigninWithExistingMobile,
            HttpMethod.Post,
            requestMapper,
            responseMapper)
    {
    }
}
sealed class SigninMobileExistsApiRequestMApper : IPresentationMapper<SigninWithExistingMobileApiRequest, SigninWithExistingMobileCommand>
{
    public ValueTask<PrimitiveResult<SigninWithExistingMobileCommand>> Map(SigninWithExistingMobileApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new SigninWithExistingMobileCommand(src.Mobile, src.Otp)));
}


sealed class SigninMobileExistsApiResponseMapper : IPresentationMapper<UserTokenInfo, SigninWithExistingMobileApiResponse>
{
    public ValueTask<PrimitiveResult<SigninWithExistingMobileApiResponse>> Map(UserTokenInfo src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new SigninWithExistingMobileApiResponse(
                    src.Token,
                    src.Fullname,
                    src.Mobile)));
}

