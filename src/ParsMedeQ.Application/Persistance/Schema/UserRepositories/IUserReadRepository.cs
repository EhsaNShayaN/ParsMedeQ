using ParsMedeQ.Domain.Aggregates.UserAggregate;
using ParsMedeQ.Domain.Persistance;
using ParsMedeQ.Domain.Types.Email;

namespace ParsMedeQ.Application.Persistance.Schema.UserRepositories;

public interface IUserReadRepository :
    IGenericDomainEntityReadRepository<User>,
    IDomainRepository
{
    ValueTask<PrimitiveResult> EnsureEmailIsUnique(int id, EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> EnsureMobileIsUnique(int id, MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindByMobile(MobileType mobile, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindByEmail(EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<User>> FindUser(int id, CancellationToken cancellationToken);
}