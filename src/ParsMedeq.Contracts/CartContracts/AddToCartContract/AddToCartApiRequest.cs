namespace ParsMedeQ.Contracts.CartContracts.AddToCartContract;
public record struct AddToCartApiRequest(
    int RelatedId,
    int TableId,
    int Quantity);
