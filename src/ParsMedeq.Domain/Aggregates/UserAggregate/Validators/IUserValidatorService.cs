using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.Mobile;

namespace ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;

public interface IUserValidatorService : IDomainValidator
{
    ValueTask<PrimitiveResult> IsEmailUnique(int id, EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> IsPhonenumberUnique(int id, MobileType mobile, CancellationToken cancellationToken);

}
