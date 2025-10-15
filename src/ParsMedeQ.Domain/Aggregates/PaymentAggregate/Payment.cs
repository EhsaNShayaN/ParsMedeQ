using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.PaymentAggregate;
public sealed class Payment : EntityBase<int>
{
    #region " Fields "
    private List<PaymentLog> _paymentLogs = [];
    #endregion

    #region " Properties "
    public int OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public byte PaymentMethod { get; private set; }
    public string? TransactionId { get; private set; }
    public byte Status { get; private set; }
    public DateTime? PaidDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Order Order { get; private set; } = null!;
    public IReadOnlyCollection<PaymentLog> PaymentLogs => this._paymentLogs.AsReadOnly();
    #endregion

    #region " Constructors "
    private Payment() : base(0) { }
    public Payment(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Payment> Create(
        int orderId,
        decimal amount,
        byte paymentMethod,
        string transactionId,
        byte status,
        DateTime? paidDate,
        DateTime creationDate)
    {
        return PrimitiveResult.Success(
            new Payment()
            {
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                TransactionId = transactionId,
                Status = status,
                PaidDate = paidDate,
                CreationDate = creationDate
            });
    }
    #endregion
}