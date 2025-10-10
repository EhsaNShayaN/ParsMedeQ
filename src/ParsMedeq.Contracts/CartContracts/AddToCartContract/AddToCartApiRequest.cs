namespace ParsMedeQ.Contracts.CartContracts.AddToCartContract;
public record struct AddToCartApiRequest(
    int? UserId,
    Guid? AnonymousId,
    int RelatedId,
    int TableId);
