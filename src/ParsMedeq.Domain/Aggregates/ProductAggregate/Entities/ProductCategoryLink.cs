using EShop.Domain.Abstractions;
using EShop.Domain.Aggregates.ProductCategoryAggregate;

namespace EShop.Domain.Aggregates.ProductAggregate.Entities;

public sealed class ProductCategoryLink : EntityBase<int>
{
    #region " Properties "
    public int ProductId { get; private set; }
    public int ProductCategoryId { get; private set; }
    public int Ordinal { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Product Product { get; private set; } = null!;
    public ProductCategory ProductCategory { get; private set; } = null!;
    #endregion

    #region " Constructors "
    public ProductCategoryLink(int id) : base(id) { }
    #endregion
}
