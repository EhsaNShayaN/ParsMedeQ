using ParsMedeq.Domain.Aggregates.UserAggregate.UserEntity;
using ParsMedeq.Domain.Persistance;
using ParsMedeq.Domain.Types.Email;
using ParsMedeq.Domain.Types.UserId;

namespace ParsMedeq.Application.Persistance.ESopSchema.UserRepositories;

public interface IUserReadRepository :
    IGenericDomainEntityReadRepository<User>,
    IDomainRepository
{
    ValueTask<PrimitiveResult> EnsureEmailIsUnique(UserIdType id, EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> EnsureMobileIsUnique(UserIdType id, MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindByMobile(MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindByEmail(EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindUser(UserIdType id, CancellationToken cancellationToken);    
}