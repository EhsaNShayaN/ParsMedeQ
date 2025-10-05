using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
public sealed class CartItem : EntityBase<int>
{
    #region " Properties "
    public int CartId { get; set; }
    public string ProductType { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; } = 1;
    public int QuantityBeforeUpdate { get; set; } = 1;
    #endregion

    #region " Navigation Properties "
    public Cart Cart { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private CartItem() : base(0) { }
    public CartItem(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<CartItem> Create(
        CartItem item) => PrimitiveResult.Success(item);
    internal static PrimitiveResult<CartItem> Create(
        string productType,
        int productId,
        string productName,
        decimal unitPrice,
        int quantity) => PrimitiveResult.Success(
            new CartItem
            {
                ProductType = productType,
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity,
            });
    internal PrimitiveResult<CartItem> Update(
        string productName,
        decimal unitPrice,
        int quantity)
    {
        this.ProductName = productName;
        this.UnitPrice = unitPrice;
        this.Quantity = quantity;
        return this;
    }
    #endregion
}
