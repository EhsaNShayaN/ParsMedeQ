namespace ParsMedeQ.Contracts.PaymentContracts.ConfirmPaymentContract;
public readonly record struct ConfirmPaymentApiResponse(string TransactionId, int OrderId, string OrderNumber, decimal Amount);