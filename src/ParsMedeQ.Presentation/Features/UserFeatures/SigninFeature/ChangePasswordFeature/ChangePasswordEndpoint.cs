using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.ChangePasswordFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.ChangePasswordContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.ChangePasswordFeature;
sealed class ChangePasswordEndpoint : EndpointHandlerBase<
    ChangePasswordApiRequest,
    ChangePasswordCommand,
    ChangePasswordCommandResponse,
    ChangePasswordApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => false;

    public ChangePasswordEndpoint(
        IPresentationMapper<ChangePasswordApiRequest, ChangePasswordCommand> apiRequestMapper
        ) : base(
            Endpoints.User.ChangePassword,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class ChangePasswordApiRequestMapper : IPresentationMapper<ChangePasswordApiRequest, ChangePasswordCommand>
{
    public ValueTask<PrimitiveResult<ChangePasswordCommand>> Map(ChangePasswordApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new ChangePasswordCommand(
                    src.OldPassword,
                    src.Password)));
}