using EShop.Domain.Abstractions;
using EShop.Domain.Aggregates.ProductAggregate.Entities;
using EShop.Domain.Aggregates.ProductTypeAggregate;

namespace EShop.Domain.Aggregates.ProductCategoryAggregate;

public sealed class ProductCategory : AggregateRoot<int>
{
    #region " Fields "
    List<ProductCategory> _children = [];
    List<ProductCategoryLink> _productLinks = [];
    #endregion

    #region " Properties "
    public int? ParentId { get; private set; }
    public int ProductTypeId { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public ProductCategory? Parent { get; private set; }
    public ProductType ProductType { get; private set; } = null!;
    public IReadOnlyCollection<ProductCategory> Children => this._children.AsReadOnly();
    public IReadOnlyCollection<ProductCategoryLink> ProductLinks => this._productLinks.AsReadOnly();
    //public IReadOnlyCollection<Product> Products => this._productLinks.Select(link => link.Product).ToList().AsReadOnly();
    #endregion

    #region " Constructors "
    public ProductCategory(int id) : base(id) { }
    #endregion

    public PrimitiveResult AddChildCategory(ProductCategory child)
    {
        if (this._children.Any(c => c.Title == child.Title))
            return PrimitiveResult.Failure("Domain.Error", "Category name must be unique under the same parent.");

        this._children.Add(child);

        return PrimitiveResult.Success();
    }
}
