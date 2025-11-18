namespace ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;

public record PaymentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;
public record AdminPaymentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;
public record UserPaymentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;