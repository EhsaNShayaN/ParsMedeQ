using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.UserAggregate;

namespace ParsMedeQ.Domain.Aggregates.PurchaseAggregate;

public sealed class Purchase : EntityBase<int>
{
    #region " Properties "
    public int UserId { get; private set; }
    public int RelatedId { get; private set; }
    public int TableId { get; private set; }
    public bool Purchased { get; private set; }
    public string Data { get; private set; } = string.Empty;
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public User User { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private Purchase() : base(0) { }
    public Purchase(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Purchase> Create(
        int userId,
        int relatedId,
        int tableId,
        bool purchased,
        string data,
        DateTime creationDate)
    {
        return PrimitiveResult.Success(
            new Purchase()
            {
                UserId = userId,
                RelatedId = relatedId,
                TableId = tableId,
                Purchased = purchased,
                Data = data,
                CreationDate = creationDate
            });
    }
    #endregion
}