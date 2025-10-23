namespace ParsMedeQ.Contracts.PaymentContracts.ConfirmPaymentContract;
public readonly record struct ConfirmPaymentApiRequest(int PaymentId, string TransactionId);