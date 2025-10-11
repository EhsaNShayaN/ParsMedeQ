namespace ParsMedeQ.Contracts.CartContracts.CartListContract;
public sealed record CartListApiResponse(
    int Id,
    CartItemListApiResponse[] CartItems);
public sealed record CartItemListApiResponse(
int TableId,
int RelatedId,
string RelatedName,
decimal UnitPrice,
int Quantity);
