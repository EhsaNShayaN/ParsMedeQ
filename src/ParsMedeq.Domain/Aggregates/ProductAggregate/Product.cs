using EShop.Domain.Abstractions;
using EShop.Domain.Aggregates.ProductAggregate.Entities;
using EShop.Domain.Aggregates.ProductCategoryAggregate;
using EShop.Domain.Aggregates.ProductTypeAggregate.Entities;

namespace EShop.Domain.Aggregates.ProductAggregate;

public sealed class Product : AggregateRoot<int>
{
    #region " Fields "
    private List<ProductCategoryLink> _categoryLinks = [];
    #endregion

    #region " Properties "
    public int ProductTypeId { get; private set; }
    public int ModelId { get; private set; }
    public string Slug { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Specifications { get; private set; } = string.Empty;

    #endregion

    #region " Navigation Properties "
    public ProductType ProductType { get; private set; } = null!;
    public ProductModel Model { get; private set; } = null!;
    public IReadOnlyCollection<ProductCategoryLink> CategoryLinks => this._categoryLinks.AsReadOnly();
    public IReadOnlyCollection<ProductCategory> Categories => this._categoryLinks.Select(link => link.ProductCategory).ToList().AsReadOnly();

    #endregion

    #region " Constructors "
    // EF
    private Product() : this(0) { }
    private Product(int id) : base(id) { }

    #region " Factory "
    public static PrimitiveResult<Product> Create(
    int productTypeId,
    int modelId,
    string slug,
    string title,
    string specifications)
    {
        return PrimitiveResult.Success(
            new Product()
            {
                ProductTypeId = productTypeId,
                ModelId = modelId,
                Slug = slug,
                Title = title,
                Specifications = specifications
            });
    } 
    #endregion
    #endregion

    #region " Methods "
    public PrimitiveResult AddCategoryLink(ProductCategoryLink link)
    {
        if (this._categoryLinks.Any(cl => cl.ProductCategoryId == link.ProductCategoryId))
            return PrimitiveResult.Failure("Domain.Error", "Product already belongs to this category.");

        this._categoryLinks.Add(link);

        return PrimitiveResult.Success();
    } 
    #endregion
}
