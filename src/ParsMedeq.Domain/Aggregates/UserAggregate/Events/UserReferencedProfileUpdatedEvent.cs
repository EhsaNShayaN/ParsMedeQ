using ParsMedeq.Domain.Aggregates.UserAggregate.UserEntity;
using ParsMedeq.Domain.Events;
using ParsMedeq.Domain.Helpers;
using ParsMedeq.Domain.Types.UserId;

namespace ParsMedeq.Domain.Aggregates.UserAggregate.Events;

public sealed record UserProfileUpdatedEvent(int Id) : IntegrationEventBase(DateHelpers.Now) { }


public sealed record UserReferencedProfileUpdatedEvent(User SourceObject) :
    ReferencedEntityEvent<User, UserIdType, int, UserProfileUpdatedEvent>(SourceObject);


