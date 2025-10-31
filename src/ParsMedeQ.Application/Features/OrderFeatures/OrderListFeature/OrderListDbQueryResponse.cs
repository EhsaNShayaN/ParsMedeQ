namespace ParsMedeQ.Application.Features.OrderFeatures.OrderListFeature;

public readonly record struct OrderListDbQueryResponse(
    int Id,
    int UserId,
    string OrderNumber,
    decimal TotalAmount,
    decimal DiscountAmount,
    decimal? FinalAmount,
    byte Status,
    string FullName,
    DateTime? UpdateDate,
    DateTime CreationDate,
    OrderItemDbQueryResponse[] Items);
public readonly record struct OrderItemDbQueryResponse(
    int TableId,
    int RelatedId,
    string RelatedName,
    int Quantity,
    decimal UnitPrice,
    decimal? Subtotal,
    DateTime? GuarantyExpirationDate,
    int PeriodicServiceInterval);