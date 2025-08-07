using EShop.Domain.Abstractions;
using EShop.Domain.Aggregates.ProductTypeAggregate.Entities;

namespace EShop.Domain.Aggregates.ProductTypeAggregate;

[Flags]
public enum ProductTypeVariations
{
    None = 0,
    Color = 1,
    Size = 1 << 1,
    Weight = 2 << 1
}


public sealed class ProductType : AggregateRoot<int>
{
    #region " Fields "
    private List<ProductBrand> _barands = [];
    #endregion

    #region " Properties "
    public string Slug { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string JsonSchema { get; private set; } = string.Empty;
    public IReadOnlyList<ProductTypeVariations> Variations { get; private set; } = [];
    #endregion

    #region " Navigation Properties "
    public IReadOnlyCollection<ProductBrand> Brands => this._barands.AsReadOnly();
    #endregion

    #region " Constructors "
    public ProductType(int id) : base(id) { }
    #endregion
}
