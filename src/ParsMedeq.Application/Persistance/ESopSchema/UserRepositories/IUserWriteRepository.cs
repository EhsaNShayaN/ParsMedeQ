using EShop.Domain.Aggregates.UserAggregate.UserEntity;
using EShop.Domain.Persistance;
using EShop.Domain.Types.UserId;

namespace EShop.Application.Persistance.ESopSchema.UserRepositories;
public interface IUserWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<User>> FindById(UserIdType id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindByMobile(MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> AddUser(User src, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> UpdateUser(User src);
    ValueTask<PrimitiveResult<User>> UpdatePassword(User src, CancellationToken cancellationToken);
}
