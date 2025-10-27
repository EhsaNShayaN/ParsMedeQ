namespace ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;
public readonly record struct PaymentListApiResponse(
    int Id,
    int OrderId,
    decimal Amount,
    byte PaymentMethod,
    string? TransactionId,
    byte Status,
    DateTime? PaidDate,
    DateTime CreationDate);