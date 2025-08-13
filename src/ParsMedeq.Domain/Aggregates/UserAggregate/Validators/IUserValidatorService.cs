using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.Mobile;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;

public interface IUserValidatorService : IDomainValidator
{
    ValueTask<PrimitiveResult> IsEmailUnique(UserIdType id, EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> IsPhonenumberUnique(UserIdType id, MobileType mobile, CancellationToken cancellationToken);

}
