namespace ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;
public readonly record struct PaymentListApiResponse(
    int Id,
    decimal Amount,
    byte PaymentMethod,
    string? TransactionId,
    byte Status,
    string StatusText,
    DateTime? PaidDate,
    DateTime CreationDate);