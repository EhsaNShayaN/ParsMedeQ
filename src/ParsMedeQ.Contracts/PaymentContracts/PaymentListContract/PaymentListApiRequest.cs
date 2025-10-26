namespace ParsMedeQ.Contracts.PaymentContracts.PaymentListContract;

public record PaymentListApiRequest(int? RelatedId) : BasePaginatedApiRequest;