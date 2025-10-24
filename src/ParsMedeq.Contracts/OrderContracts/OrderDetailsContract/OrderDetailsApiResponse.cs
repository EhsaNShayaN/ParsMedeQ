namespace ParsMedeQ.Contracts.OrderContracts.OrderDetailsContract;
public readonly record struct OrderDetailsApiResponse(
    int Id,
    int PaymentId,
    string OrderNumber,
    decimal TotalAmount,
    decimal DiscountAmount,
    decimal? FinalAmount,
    byte Status,
    DateTime? UpdateDate,
    DateTime CreationDate,
    OrderItemApiResponse[] Items);
public readonly record struct OrderItemApiResponse(
    int OrderId,
    int TableId,
    int RelatedId,
    string RelatedName,
    int Quantity,
    decimal UnitPrice,
    decimal? Subtotal);