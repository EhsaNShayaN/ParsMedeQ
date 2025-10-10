namespace ParsMedeQ.Contracts.CartContracts.CartListContract;

public record CartListApiRequest(
    int? UserId,
    Guid? AnonymousId);