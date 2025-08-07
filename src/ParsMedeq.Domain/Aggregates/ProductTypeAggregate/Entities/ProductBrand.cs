using EShop.Domain.Abstractions;

namespace EShop.Domain.Aggregates.ProductTypeAggregate.Entities;

public sealed class ProductBrand : EntityBase<int>
{
    #region " Fields "
    private List<ProductModel> _models = [];
    #endregion

    #region " Properties "
    public int ProductTypeId { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public ProductType ProductType { get; private set; } = null!;
    public IReadOnlyCollection<ProductModel> Models => this._models.AsReadOnly();
    #endregion

    #region " Constructors "
    public ProductBrand(int id) : base(id) { }
    #endregion
}
