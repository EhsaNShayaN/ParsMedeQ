namespace ParsMedeQ.Contracts.PaymentContracts.AddPaymentContract;
public readonly record struct AddPaymentApiRequest(int OrderId, decimal Amount);