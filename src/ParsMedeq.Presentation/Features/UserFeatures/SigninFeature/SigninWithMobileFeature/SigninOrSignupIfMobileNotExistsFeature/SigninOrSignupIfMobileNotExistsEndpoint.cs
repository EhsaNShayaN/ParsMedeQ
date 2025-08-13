using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninOrSignupIfMobileNotExistsFeature;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninOrSignupIfMobileNotExistsFeature;

internal sealed class SigninOrSignupIfMobileNotExistsEndpoint : EndpointHandlerBase<
    SigninOrSignupIfMobileNotExistsApiRequest,
    SigninOrSignupIfMobileNotExistsCommand,
    UserTokenInfo,
    SigninOrSignupIfMobileNotExistsApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SigninOrSignupIfMobileNotExistsEndpoint(
        IPresentationMapper<SigninOrSignupIfMobileNotExistsApiRequest, SigninOrSignupIfMobileNotExistsCommand> requestMapper,
        IPresentationMapper<UserTokenInfo, SigninOrSignupIfMobileNotExistsApiResponse> responseMapper) : base(
       Endpoints.User.SigninWithMobile,
       HttpMethod.Post,
       requestMapper,
       responseMapper)
    {
    }
}
sealed class SigninOrSignupIfMobileNotExistsApiRequestMApper : IPresentationMapper<SigninOrSignupIfMobileNotExistsApiRequest, SigninOrSignupIfMobileNotExistsCommand>
{
    public ValueTask<PrimitiveResult<SigninOrSignupIfMobileNotExistsCommand>> Map(SigninOrSignupIfMobileNotExistsApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new SigninOrSignupIfMobileNotExistsCommand(src.Mobile, src.Otp)));
}


sealed class SigninOrSignupIfMobileNotExistsApiResponseMapper : IPresentationMapper<UserTokenInfo, SigninOrSignupIfMobileNotExistsApiResponse>
{
    public ValueTask<PrimitiveResult<SigninOrSignupIfMobileNotExistsApiResponse>> Map(UserTokenInfo src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new SigninOrSignupIfMobileNotExistsApiResponse(
                    src.Token,
                    src.Fullname,
                    src.Mobile)));
}

