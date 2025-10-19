using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.UserRepositories;
public interface IUserWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<User>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindByMobile(MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> AddUser(User src, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> UpdateUser(User src);
    ValueTask<PrimitiveResult<User>> UpdatePassword(User src, CancellationToken cancellationToken);
}
