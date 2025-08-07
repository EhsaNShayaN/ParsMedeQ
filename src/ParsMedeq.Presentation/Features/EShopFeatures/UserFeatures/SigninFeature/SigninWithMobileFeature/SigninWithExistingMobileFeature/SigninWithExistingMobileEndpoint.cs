using EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithExistingMobileFeature;
using EShop.Application.Services.UserContextAccessorServices;
using EShop.Contracts;
using EShop.Contracts.UserContracts.SigninContracts.SigninWithMobileContracts.VerifyOtpContracts;

namespace EShop.Presentation.Features.EShopFeatures.UserFeatures.SigninFeature.SigninWithMobileFeature.SigninWithExistingMobileFeature;

internal sealed class SigninWithExistingMobileEndpoint : EndpointHandlerBase<
    SigninWithExistingMobileApiRequest,
    SigninWithExistingMobileCommand,
    UserTokenInfo,
    SigninWithExistingMobileApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public SigninWithExistingMobileEndpoint(
        IPresentationMapper<SigninWithExistingMobileApiRequest, SigninWithExistingMobileCommand> requestMapper,
        IPresentationMapper<UserTokenInfo, SigninWithExistingMobileApiResponse> responseMapper) : base(
            EShopEndpoints.User.SigninWithExistingMobile,
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

