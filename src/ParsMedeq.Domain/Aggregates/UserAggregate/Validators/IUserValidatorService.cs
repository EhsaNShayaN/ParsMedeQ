using ParsMedeq.Domain.Abstractions;
using ParsMedeq.Domain.Types.Email;
using ParsMedeq.Domain.Types.Mobile;
using ParsMedeq.Domain.Types.UserId;

namespace ParsMedeq.Domain.Aggregates.UserAggregate.Validators;

public interface IUserValidatorService : IDomainValidator
{
    ValueTask<PrimitiveResult> IsEmailUnique(UserIdType id, EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> IsPhonenumberUnique(UserIdType id, MobileType mobile, CancellationToken cancellationToken);

}
