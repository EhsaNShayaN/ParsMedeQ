using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;

namespace ParsMedeQ.Domain.Aggregates.ProductAggregate;

public sealed class Product : EntityBase<int>
{
    #region " Properties "
    public int ProductCategoryId { get; private set; }
    public string ProductCategoryTitle { get; private set; } = string.Empty;
    public int Sequential { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public string Video { get; private set; } = string.Empty;
    public string Doc { get; private set; } = string.Empty;
    public int? Price { get; private set; }
    public int? Discount { get; private set; }
    public int DownloadCount { get; private set; }
    public int VisitCount { get; private set; }
    public int SaleCount { get; private set; }
    public bool Stock { get; private set; }
    public bool Deleted { get; private set; }
    public bool Disabled { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public ProductCategory ProductCategory { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private Product() : base(0) { }
    private Product(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Product> Create(
        int productCategoryId,
        string productCategoryTitle,
        int sequential,
        string title,
        string @abstract,
        string anchors,
        string description,
        string image,
        string video,
        string doc,
        int price,
        int discount,
        int downloadCount,
        int visitCount,
        int saleCount,
        bool stock,
        bool deleted,
        bool disabled,
        DateTime creationDate)
    {
        return PrimitiveResult.Success(
            new Product()
            {
                ProductCategoryId = productCategoryId,
                ProductCategoryTitle = productCategoryTitle,
                Sequential = sequential,
                Title = title,
                Abstract = @abstract,
                Anchors = anchors,
                Description = description,
                Image = image,
                Video = video,
                Doc = doc,
                Price = price,
                Discount = discount,
                DownloadCount = downloadCount,
                VisitCount = visitCount,
                SaleCount = saleCount,
                Stock = stock,
                Deleted = deleted,
                Disabled = disabled,
                CreationDate = creationDate
            });
    }
    #endregion

}