using ParsMedeQ.Application.Features.UserFeatures.SigninFeature.SetPasswordFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.SigninContracts.SetPasswordContracts;

namespace ParsMedeQ.Presentation.Features.UserFeatures.SigninFeature.SetPasswordFeature;
sealed class SetPasswordEndpoint : EndpointHandlerBase<
    SetPasswordApiRequest,
    SetPasswordCommand,
    SetPasswordCommandResponse,
    SetPasswordApiResponse>
{
    protected override bool NeedAuthentication => true;
    protected override bool NeedTaxPayerFile => false;

    public SetPasswordEndpoint(
        IPresentationMapper<SetPasswordApiRequest, SetPasswordCommand> apiRequestMapper
        ) : base(
            Endpoints.User.SetPassword,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class SetPasswordApiRequestMapper : IPresentationMapper<SetPasswordApiRequest, SetPasswordCommand>
{
    public ValueTask<PrimitiveResult<SetPasswordCommand>> Map(SetPasswordApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new SetPasswordCommand(src.Password)));
}