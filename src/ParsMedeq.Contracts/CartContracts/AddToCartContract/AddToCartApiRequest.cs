namespace ParsMedeQ.Contracts.CartContracts.AddToCartContract;
public record struct AddToCartApiRequest(
    Guid AnonymousId,
    int RelatedId,
    int TableId,
    int Quantity);
