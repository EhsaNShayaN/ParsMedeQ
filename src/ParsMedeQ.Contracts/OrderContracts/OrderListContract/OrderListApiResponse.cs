namespace ParsMedeQ.Contracts.OrderContracts.OrderListContract;
public readonly record struct OrderListApiResponse(
    int Id,
    int UserId,
    string OrderNumber,
    decimal TotalAmount,
    decimal DiscountAmount,
    decimal? FinalAmount,
    byte Status,
    DateTime? UpdateDate,
    DateTime CreationDate);