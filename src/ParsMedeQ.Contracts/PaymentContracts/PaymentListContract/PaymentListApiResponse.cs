namespace ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;
public readonly record struct PaymentListApiResponse(
    int Id,
    decimal Amount,
    byte PaymentMethod,
    string? TransactionId,
    byte Status,
    string StatusText,
    string FullName,
    DateTime? PaidDate,
    DateTime CreationDate);