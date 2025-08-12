using ParsMedeq.Domain.Aggregates.UserAggregate.UserEntity;
using ParsMedeq.Domain.Persistance;
using ParsMedeq.Domain.Types.UserId;

namespace ParsMedeq.Application.Persistance.ESopSchema.UserRepositories;
public interface IUserWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<User>> FindById(UserIdType id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindByMobile(MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> AddUser(User src, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> UpdateUser(User src);
    ValueTask<PrimitiveResult<User>> UpdatePassword(User src, CancellationToken cancellationToken);
}
