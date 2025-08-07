using EShop.Domain.Abstractions;
using EShop.Domain.Aggregates.ProductAggregate;

namespace EShop.Domain.Aggregates.ProductTypeAggregate.Entities;

public sealed class ProductModel : EntityBase<int>
{
    #region " Fields "
    private List<Product> _products = [];
    #endregion

    #region " Properties "
    public int ProductBrandId { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public ProductBrand Brand { get; private set; } = null!;
    public IReadOnlyCollection<Product> Products => this._products.AsReadOnly();
    #endregion

    #region " Constructors "
    public ProductModel(int id) : base(id) { }
    #endregion
}
