using ParsMedeq.Domain.Abstractions;
using ParsMedeq.Domain.Aggregates.ProductAggregate;

namespace ParsMedeq.Domain.Aggregates.ProductCategoryAggregate;

public sealed class ProductCategory : EntityBase<int>
{
    #region " Fields "
    private List<Product> _products = [];
    private List<ProductCategory> _productCategories = [];
    #endregion

    #region " Properties "
    public string Title { get; private set; } = string.Empty;
    public int? ParentId { get; private set; }
    public int Sequential { get; private set; }
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Cover { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public bool Deleted { get; private set; }
    public bool Disabled { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public ProductCategory? Parent { get; private set; }
    public IReadOnlyCollection<Product> Products => this._products.AsReadOnly();
    public IReadOnlyCollection<ProductCategory> Children => this._productCategories.AsReadOnly();
    #endregion

    #region " Constructors "
    private ProductCategory() : base(0) { }
    public ProductCategory(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<ProductCategory> Create(
        string title,
        int parentId,
        int sequential,
        string @abstract,
        string anchors,
        string description,
        string cover,
        string image,
        string translation,
        bool deleted,
        bool disabled,
        DateTime creationDate)
    {
        return PrimitiveResult.Success(
            new ProductCategory()
            {
                Title = title,
                ParentId = parentId,
                Sequential = sequential,
                Abstract = @abstract,
                Anchors = anchors,
                Description = description,
                Cover = cover,
                Image = image,
                Deleted = deleted,
                Disabled = disabled,
                CreationDate = creationDate
            });
    }
    #endregion
}
