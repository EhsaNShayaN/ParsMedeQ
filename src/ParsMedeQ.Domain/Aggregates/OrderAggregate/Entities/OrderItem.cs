using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.OrderAggregate.Entities;
public sealed class OrderItem : EntityBase<int>
{
    #region " Fields "
    private List<PeriodicService> _periodicServices = [];
    #endregion

    #region " Properties "
    public int OrderId { get; private set; }
    public int TableId { get; private set; }
    public int RelatedId { get; private set; }
    public string RelatedName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal? Subtotal { get; private set; }
    public DateTime? GuarantyExpirationDate { get; private set; }
    public int PeriodicServiceInterval { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Order Order { get; private set; } = null!;
    public IReadOnlyCollection<PeriodicService> PeriodicServices => this._periodicServices.AsReadOnly();
    #endregion

    #region " Constructors "
    private OrderItem() : base(0) { }
    public OrderItem(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<OrderItem> Create(
        int tableId,
        int relatedId,
        string relatedName,
        int quantity,
        decimal unitPrice,
        decimal subtotal,
        DateTime? guarantyExpirationDate,
        int periodicServiceInterval)
    {
        return PrimitiveResult.Success(
            new OrderItem()
            {
                TableId = tableId,
                RelatedId = relatedId,
                RelatedName = relatedName,
                Quantity = quantity,
                UnitPrice = unitPrice,
                Subtotal = subtotal,
                GuarantyExpirationDate = guarantyExpirationDate,
                PeriodicServiceInterval = periodicServiceInterval
            });
    }

    public ValueTask<PrimitiveResult<PeriodicService>> AddPeriodicService(DateTime fromDate)
    {
        return PeriodicService.Create(fromDate.AddMonths(this.PeriodicServiceInterval))
            .OnSuccess(periodicService => this._periodicServices.Add(periodicService.Value));
    }

    public PrimitiveResult DonePeriodicService(int id)
    {
        _periodicServices
            .FirstOrDefault(s => s.Id.Equals(id))
            .DoneService();
        return PrimitiveResult.Success();
    }
    #endregion
}