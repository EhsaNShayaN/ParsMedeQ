using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsMedeQ.Domain.Aggregates.ProductAggregate;
public sealed class Product : EntityBase<int>
{
    #region " Fields "
    private List<ProductTranslation> _productTranslations = [];
    #endregion

    #region " Properties "
    public int ProductCategoryId { get; private set; }
    public string Language { get; private set; } = string.Empty;
    public string PublishDate { get; private set; } = string.Empty;
    public string PublishInfo { get; private set; } = string.Empty;
    public string Publisher { get; private set; } = string.Empty;
    public int? Price { get; private set; }
    public int? Discount { get; private set; }
    public bool IsVip { get; private set; }
    public int DownloadCount { get; private set; }
    public int? Ordinal { get; private set; }
    public bool Deleted { get; private set; }
    public bool Disabled { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    [NotMapped]
    public bool Registered { get; set; }
    #endregion

    #region " Navigation Properties "
    public ProductCategory? ProductCategory { get; private set; }
    [NotMapped]
    public ProductCategory[]? ProductCategories { get; set; }
    public IReadOnlyCollection<ProductTranslation> ProductTranslations => this._productTranslations.AsReadOnly();
    #endregion

    #region " Constructors "
    private Product() : base(0) { }
    public Product(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Product> Create(
        int ProductCategoryId,
        string language,
        string publishDate,
        string publishInfo,
        string publisher,
        int price,
        int discount,
        bool isVip,
        DateTime? expirationDate) => PrimitiveResult.Success(
            new Product
            {
                ProductCategoryId = ProductCategoryId,
                Language = language,
                PublishDate = publishDate,
                PublishInfo = publishInfo,
                Publisher = publisher,
                Price = price,
                Discount = discount,
                IsVip = isVip,
                DownloadCount = 0,
                ExpirationDate = expirationDate,
                CreationDate = DateTime.Now
            });

    public ValueTask<PrimitiveResult<Product>> Update(
        int ProductCategoryId,
        string language,
        string publishDate,
        string publishInfo,
        string publisher,
        int price,
        int discount,
        bool isVip,
        DateTime? expirationDate)
    {
        this.ProductCategoryId = ProductCategoryId;
        this.Language = language;
        this.PublishDate = publishDate;
        this.PublishInfo = publishInfo;
        this.Publisher = publisher;
        this.Price = price;
        this.Discount = discount;
        this.IsVip = isVip;
        this.ExpirationDate = expirationDate;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }

    public ValueTask<PrimitiveResult<Product>> Update(
        int ProductCategoryId,
        string language,
        string publishDate,
        string publishInfo,
        string publisher,
        int price,
        int discount,
        bool isVip,
        DateTime? expirationDate,
        string langCode,
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords,
        string imagePath,
        int? fileId)
    {
        return this.Update(ProductCategoryId, language, publishDate, publishInfo, publisher, price, discount, isVip, expirationDate)
             .Map(_ => this.UpdateTranslation(langCode, title, description, @abstract, anchors, keywords, imagePath, fileId).Map(() => this));
    }
    #endregion

    public ValueTask<PrimitiveResult> AddTranslation(
        string langCode,
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords,
        string imagePath,
        int? fileId)
    {
        return ProductTranslation.Create(langCode, title, description, @abstract, anchors, keywords, imagePath, fileId)
            .OnSuccess(newTranslation => this._productTranslations.Add(newTranslation.Value))
            .Match(
                success => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }

    public ValueTask<PrimitiveResult> UpdateTranslation(
        string langCode,
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords,
        string imagePath,
        int? fileId)
    {
        var currentTranslation = _productTranslations.FirstOrDefault(s => s.LanguageCode.Equals(langCode, StringComparison.OrdinalIgnoreCase));
        if (currentTranslation is null)
        {
            return this.AddTranslation(langCode, title, description, @abstract, anchors, keywords, imagePath, fileId);
        }
        return currentTranslation.Update(title, description, @abstract, anchors, keywords, imagePath, fileId)
            .Match(
                _ => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }
}
