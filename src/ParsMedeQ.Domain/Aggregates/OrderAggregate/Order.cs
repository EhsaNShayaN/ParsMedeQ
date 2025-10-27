using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.OrderAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate;
using ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;

namespace ParsMedeQ.Domain.Aggregates.OrderAggregate;
public sealed class Order : EntityBase<int>
{
    #region " Fields "
    private List<OrderItem> _orderItems = [];
    private Payment? _payment = null;
    #endregion

    #region " Properties "
    public int UserId { get; private set; }
    public string OrderNumber { get; private set; } = string.Empty;
    public decimal TotalAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal? FinalAmount { get; private set; }
    public byte Status { get; private set; }
    public DateTime? UpdateDate { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    #endregion

    #region " Navigation Properties "
    public User User { get; private set; } = null!;
    public IReadOnlyCollection<OrderItem> OrderItems => this._orderItems.AsReadOnly();
    public Payment? Payment => this._payment;
    #endregion

    #region " Constructors "
    private Order() : base(0) { }
    public Order(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Order> Create(
        int userId,
        decimal totalAmount,
        decimal discountAmount,
        byte status,
        string orderNumber)
    {
        return PrimitiveResult.Success(
            new Order()
            {
                UserId = userId,
                TotalAmount = totalAmount,
                DiscountAmount = discountAmount,
                Status = status,
                OrderNumber = orderNumber,
                CreationDate = DateTime.UtcNow
            });
    }

    public ValueTask<PrimitiveResult> AddItem(
        int tableId,
        int relatedId,
        string relatedName,
        int quantity,
        decimal unitPrice,
        decimal subtotal)
    {
        return OrderItem.Create(
            tableId,
            relatedId,
            relatedName,
            quantity,
            unitPrice,
            subtotal)
            .OnSuccess(item => this._orderItems.Add(item.Value))
            .Match(
            success => PrimitiveResult.Success(),
            PrimitiveResult.Failure);
    }


    public ValueTask<PrimitiveResult<Order>> Update(
        byte status)
    {
        this.UpdateDate = DateTime.UtcNow;
        this.Status = status;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }
    #endregion
}