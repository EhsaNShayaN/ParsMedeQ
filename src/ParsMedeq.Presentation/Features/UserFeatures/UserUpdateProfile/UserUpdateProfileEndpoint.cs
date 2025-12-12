using ParsMedeQ.Application.Features.UserFeatures.UserUpdateProfile;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.UserUpdateProfile;

namespace ParsMedeQ.Presentation.Features.UserFeatures.UserUpdateProfile;

sealed class UserUpdateProfileEndpoint : EndpointHandlerBase<
    UserUpdateProfileApiRequest,
    UserUpdateProfileCommand,
    UserUpdateProfileCommandResponse,
    UserUpdateProfileApiResponse>
{
    protected override bool NeedTaxPayerFile => false;

    public UserUpdateProfileEndpoint(
        IPresentationMapper<UserUpdateProfileApiRequest, UserUpdateProfileCommand> apiRequestMapper,
         IPresentationMapper<UserUpdateProfileCommandResponse, UserUpdateProfileApiResponse> apiResponseMapper
        ) : base(
            Endpoints.User.UpdateProfile,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class UserUpdateProfileApiRequestMapper : IPresentationMapper<UserUpdateProfileApiRequest, UserUpdateProfileCommand>
{
    private readonly IUserContextAccessor _tspUserContextAccessor;

    public UserUpdateProfileApiRequestMapper(IUserContextAccessor tspUserContextAccessor)
    {
        this._tspUserContextAccessor = tspUserContextAccessor;
    }
    public ValueTask<PrimitiveResult<UserUpdateProfileCommand>> Map(UserUpdateProfileApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new UserUpdateProfileCommand(
                    this._tspUserContextAccessor.GetCurrent().UserId,
                    src.FirstName,
                    src.LastName,
                    src.NationalCode,
                    src.Email)));

}
internal sealed class UserUpdateProfileApiResponseMapper : IPresentationMapper<
    UserUpdateProfileCommandResponse,
    UserUpdateProfileApiResponse>
{
    public ValueTask<PrimitiveResult<UserUpdateProfileApiResponse>> Map(UserUpdateProfileCommandResponse src, CancellationToken cancellationToken)
    {
        return
            ValueTask.FromResult(
                PrimitiveResult.Success(new UserUpdateProfileApiResponse(src.Changed)));
    }
}