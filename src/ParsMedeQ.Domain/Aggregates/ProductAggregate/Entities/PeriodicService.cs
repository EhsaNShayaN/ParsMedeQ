using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.UserAggregate;

namespace ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;

public sealed class PeriodicService : EntityBase<int>
{
    #region " Properties "
    public int UserId { get; private set; }
    public int ProductId { get; private set; }
    public DateTime ServiceDate { get; private set; }
    public bool Done { get; private set; }
    public bool HasNext { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Product Product { get; private set; } = null!;
    public User User { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private PeriodicService() : base(0) { }
    public PeriodicService(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<PeriodicService> Create(
        int userId,
        int productId,
        DateTime serviceDate)
    {
        return PrimitiveResult.Success(
            new PeriodicService()
            {
                UserId = userId,
                ProductId = productId,
                ServiceDate = serviceDate,
                CreationDate = DateTime.Now,
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
