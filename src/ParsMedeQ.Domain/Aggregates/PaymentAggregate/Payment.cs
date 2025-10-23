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
        byte paymentMethod)
    {
        return PrimitiveResult.Success(
            new Payment()
            {
                OrderId = orderId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                Status = (byte)PaymentStatus.Pending.GetHashCode(),
                CreationDate = DateTime.UtcNow
            });
    }

    public ValueTask<PrimitiveResult<Payment>> ConfirmPayment(
        string transactionId)
    {
        this.TransactionId = transactionId;
        this.Status = (byte)PaymentStatus.Success.GetHashCode();
        this.PaidDate = DateTime.UtcNow;
        this.UpdateOrder((byte)OrderStatus.Paid.GetHashCode());
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }

    public ValueTask<PrimitiveResult<Payment>> FailPayment()
    {
        this.Status = (byte)PaymentStatus.Failed.GetHashCode();
        this.UpdateOrder((byte)OrderStatus.Pending.GetHashCode());
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }
    public ValueTask<PrimitiveResult> UpdateOrder(byte status)
    {
        var x = this.Order.Update(status);
        return ValueTask.FromResult(PrimitiveResult.Success());
    }
    #endregion
}