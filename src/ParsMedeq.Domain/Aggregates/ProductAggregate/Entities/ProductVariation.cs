using EShop.Domain.Abstractions;

namespace EShop.Domain.Aggregates.ProductAggregate.Entities;

public sealed class ProductVariation : EntityBase<int>
{
    #region " Properties "
    public int ProductId { get; private set; }
    public ProductTypeVariations VariationType { get; private set; } = ProductTypeVariations.None;
    public string Value { get; private set; } = string.Empty;
    public string InternalValue { get; private set; } = string.Empty;
    public int Stock { get; private set; }
    public MoneyInfo Price { get; private set; } = MoneyInfo.Zero;
    #endregion

    #region " Navigation Properties "
    public Product Product { get; private set; } = null!;
    #endregion

    #region " Constructors "
    public ProductVariation(int id) : base(id) { }
    #endregion
}