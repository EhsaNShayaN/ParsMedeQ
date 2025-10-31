namespace ParsMedeQ.Contracts.OrderContracts.OrderListContract;
public readonly record struct OrderListApiResponse(
    int Id,
    int UserId,
    string OrderNumber,
    decimal TotalAmount,
    decimal DiscountAmount,
    decimal? FinalAmount,
    byte Status,
    string StatusText,
    string FullName,
    DateTime? UpdateDate,
    DateTime CreationDate,
    OrderItemListApiResponse[] Items);
public readonly record struct OrderItemListApiResponse(
    int TableId,
    int RelatedId,
    string RelatedName,
    int Quantity,
    decimal UnitPrice,
    decimal? Subtotal,
    string GuarantyExpirationDate,
    int PeriodicServiceInterval);