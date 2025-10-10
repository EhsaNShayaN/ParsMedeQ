namespace ParsMedeQ.Contracts.CartContracts.MergeCartContract;
public record struct MergeCartApiRequest(
    int UserId,
    Guid AnonymousId);
