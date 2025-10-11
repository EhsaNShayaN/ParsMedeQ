namespace ParsMedeQ.Contracts.CartContracts.DeleteFromCartContract;
public readonly record struct RemoveFromCartApiRequest(
    Guid AnonymousId,
    int RelatedId);