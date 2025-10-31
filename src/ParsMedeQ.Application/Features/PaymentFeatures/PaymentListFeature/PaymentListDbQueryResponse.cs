namespace ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;

public readonly record struct PaymentListDbQueryResponse(
    int Id,
    decimal Amount,
    byte PaymentMethod,
    string? TransactionId,
    byte Status,
    string FullName,
    DateTime? PaidDate,
    DateTime CreationDate);