using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Aggregates.UserAggregate;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.UserFeatures.UserDetailsFeature;
public sealed record UserDetailsQuery() : IPrimitiveResultQuery<User>;

sealed class UserDetailsQueryHandler : IPrimitiveResultQueryHandler<UserDetailsQuery, User>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public UserDetailsQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork, IUserContextAccessor userContextAccessor)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<User>> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
    {
        return await this._readUnitOfWork.UserReadRepository.FindUser(
            _userContextAccessor.GetCurrent().UserId,
            cancellationToken)
            .ConfigureAwait(false);
    }
}