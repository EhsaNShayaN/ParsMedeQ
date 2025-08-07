using EShop.Domain.Aggregates.UserAggregate.UserEntity;
using EShop.Domain.Events;
using EShop.Domain.Helpers;
using EShop.Domain.Types.UserId;

namespace EShop.Domain.Aggregates.UserAggregate.Events;

public sealed record UserProfileUpdatedEvent(int Id) : IntegrationEventBase(DateHelpers.Now) { }


public sealed record UserReferencedProfileUpdatedEvent(User SourceObject) :
    ReferencedEntityEvent<User, UserIdType, int, UserProfileUpdatedEvent>(SourceObject);


