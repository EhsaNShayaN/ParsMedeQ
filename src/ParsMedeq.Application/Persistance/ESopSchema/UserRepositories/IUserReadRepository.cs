using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;
using ParsMedeQ.Domain.Persistance;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Application.Persistance.ESopSchema.UserRepositories;

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