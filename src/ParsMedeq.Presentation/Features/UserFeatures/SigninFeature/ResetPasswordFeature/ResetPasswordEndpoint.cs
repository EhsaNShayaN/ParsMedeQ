using ParsMedeq.Application.Features.UserFeatures.SigninFeature.ResetPasswordFeature;
using ParsMedeq.Contracts;
using ParsMedeq.Contracts.UserContracts.SigninContracts.ResetPasswordContracts;

namespace ParsMedeq.Presentation.Features.UserFeatures.SigninFeature.ResetPasswordFeature;
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
            Endpoints.User.ResetPassword,
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