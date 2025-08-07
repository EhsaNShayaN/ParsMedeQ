using EShop.Application.Features.EShopFeatures.UserFeatures.SigninFeature.ResetPasswordFeature;
using EShop.Contracts;
using EShop.Contracts.UserContracts.SigninContracts.ResetPasswordContracts;

namespace EShop.Presentation.Features.EShopFeatures.UserFeatures.SigninFeature.ResetPasswordFeature;
sealed class ResetPasswordEndpoint : EndpointHandlerBase<
    ResetPasswordApiRequest,
    ResetPasswordCommand,
    ResetPasswordCommandResponse,
    ResetPasswordApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public ResetPasswordEndpoint(
        IPresentationMapper<ResetPasswordApiRequest, ResetPasswordCommand> apiRequestMapper
        ) : base(
            EShopEndpoints.User.ResetPassword,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class ResetPasswordApiRequestMapper : IPresentationMapper<ResetPasswordApiRequest, ResetPasswordCommand>
{
    public ValueTask<PrimitiveResult<ResetPasswordCommand>> Map(ResetPasswordApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new ResetPasswordCommand(
                    src.Mobile,
                    src.Otp,
                    src.Password)));
}