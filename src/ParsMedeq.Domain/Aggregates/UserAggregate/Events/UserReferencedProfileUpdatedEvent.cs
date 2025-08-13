using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;
using ParsMedeQ.Domain.Events;
using ParsMedeQ.Domain.Helpers;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Domain.Aggregates.UserAggregate.Events;

public sealed record UserProfileUpdatedEvent(int Id) : IntegrationEventBase(DateHelpers.Now) { }


public sealed record UserReferencedProfileUpdatedEvent(User SourceObject) :
    ReferencedEntityEvent<User, UserIdType, int, UserProfileUpdatedEvent>(SourceObject);


