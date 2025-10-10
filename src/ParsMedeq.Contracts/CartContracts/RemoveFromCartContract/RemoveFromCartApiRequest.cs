namespace ParsMedeQ.Contracts.CartContracts.DeleteFromCartContract;
public readonly record struct RemoveFromCartApiRequest(
    int? UserId,
    Guid? AnonymousId,
    int RelatedId);