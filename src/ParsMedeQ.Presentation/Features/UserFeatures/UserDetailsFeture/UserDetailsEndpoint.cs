using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.UserFeatures.UserDetailsFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.UserContracts.UserDetailsContract;
using User = ParsMedeQ.Domain.Aggregates.UserAggregate.User;

namespace ParsMedeQ.Presentation.Features.UserFeatures.UserDetailsFeture;

sealed class UserDetailsEndpoint : EndpointHandlerBase<
    UserDetailsApiRequest,
    UserDetailsQuery,
    User,
    UserDetailsApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public UserDetailsEndpoint(
        IPresentationMapper<UserDetailsApiRequest, UserDetailsQuery> requestMapper,
        IPresentationMapper<User, UserDetailsApiResponse> responseMapper)
        : base(
            Endpoints.User.Details,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] UserDetailsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new UserDetailsQuery())),
            cancellationToken);
}
sealed class UserDetailsApiRequestMapper : IPresentationMapper<
    UserDetailsApiRequest,
    UserDetailsQuery>
{
    public ValueTask<PrimitiveResult<UserDetailsQuery>> Map(
        UserDetailsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new UserDetailsQuery()));
    }
}
sealed class UserDetailsApiResponseMapper : IPresentationMapper<
    User,
    UserDetailsApiResponse>
{
    public ValueTask<PrimitiveResult<UserDetailsApiResponse>> Map(
        User src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new UserDetailsApiResponse(
                        src.Id,
                        src.FullName.FirstName.Value,
                        src.FullName.LastName.Value,
                        src.FullName.GetValue(),
                        src.Email.GetValue(),
                        src.Mobile.Value,
                        src.NationalCode,
                        string.IsNullOrWhiteSpace(src.Password.Value),
                        src.IsEmailConfirmed,
                        src.IsMobileConfirmed)));
    }
}