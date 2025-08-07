using EShop.Domain.Abstractions;
using EShop.Domain.Types.Email;
using EShop.Domain.Types.Mobile;
using EShop.Domain.Types.UserId;

namespace EShop.Domain.Aggregates.UserAggregate.Validators;

public interface IUserValidatorService : IDomainValidator
{
    ValueTask<PrimitiveResult> IsEmailUnique(UserIdType id, EmailType email, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult> IsPhonenumberUnique(UserIdType id, MobileType mobile, CancellationToken cancellationToken);

}
