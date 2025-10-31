using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
public sealed class CartItem : EntityBase<int>
{
    #region " Properties "
    public int CartId { get; set; }
    public int TableId { get; set; }
    public int RelatedId { get; set; }
    public string RelatedName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    #endregion

    #region " Navigation Properties "
    public Cart Cart { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private CartItem() : base(0) { }
    public CartItem(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<CartItem> Create(CartItem item)
        => PrimitiveResult.Success(item);
    internal static PrimitiveResult<CartItem> Create(
        int tableId,
        int relatedId,
        string relatedName,
        decimal unitPrice,
        int quantity) => PrimitiveResult.Success(
            new CartItem
            {
                TableId = tableId,
                RelatedId = relatedId,
                RelatedName = relatedName,
                UnitPrice = unitPrice,
                Quantity = quantity
            });
    internal PrimitiveResult<CartItem> Update(
        string productName,
        decimal unitPrice,
        int quantity)
    {
        this.RelatedName = productName;
        this.UnitPrice = unitPrice;
        this.Quantity = quantity;
        return this;
    }
    #endregion
}
