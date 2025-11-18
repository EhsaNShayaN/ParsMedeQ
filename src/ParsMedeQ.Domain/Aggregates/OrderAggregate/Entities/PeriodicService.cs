using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.OrderAggregate.Entities;

public sealed class PeriodicService : EntityBase<int>
{
    #region " Properties "
    public int OrderItemId { get; private set; }
    public DateTime ServiceDate { get; private set; }
    public bool Done { get; private set; }
    public bool HasNext { get; private set; }
    #endregion

    #region " Navigation Properties "
    public OrderItem OrderItem { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private PeriodicService() : base(0) { }
    public PeriodicService(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<PeriodicService> Create(
        DateTime serviceDate)
    {
        return PrimitiveResult.Success(
            new PeriodicService()
            {
                ServiceDate = serviceDate,
            });
    }
    internal PrimitiveResult<PeriodicService> DoneService()
    {
        this.Done = true;
        return this;
    }
    internal PrimitiveResult<PeriodicService> NextService()
    {
        this.HasNext = true;
        return this;
    }
    #endregion
}
