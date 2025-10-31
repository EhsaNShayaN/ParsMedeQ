using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsMedeQ.Domain.Aggregates.ProductAggregate;
public sealed class Product : EntityBase<int>
{
    #region " Fields "
    private List<ProductTranslation> _productTranslations = [];
    private List<ProductMedia> _productMediaList = [];
    private List<PeriodicService> _periodicServices = [];
    #endregion

    #region " Properties "
    public int ProductCategoryId { get; private set; }
    public int Price { get; private set; }
    public int Discount { get; private set; }
    public int Stock { get; private set; }
    public int GuarantyExpirationTime { get; private set; }//زمان انقضای گارانتی
    public int PeriodicServiceInterval { get; private set; }//فاصله زمانی سرویس دوره‌ای
    public bool Deleted { get; private set; }
    public bool Disabled { get; private set; }
    public DateTime CreationDate { get; private set; }
    [NotMapped]
    public bool Registered { get; set; }
    #endregion

    #region " Navigation Properties "
    public ProductCategory? ProductCategory { get; private set; }
    [NotMapped]
    public ProductCategory[]? ProductCategories { get; set; }
    public IReadOnlyCollection<ProductTranslation> ProductTranslations => this._productTranslations.AsReadOnly();
    public IReadOnlyCollection<ProductMedia> ProductMediaList => this._productMediaList.AsReadOnly();
    public IReadOnlyCollection<PeriodicService> PeriodicServices => this._periodicServices.AsReadOnly();
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
        int guarantyExpirationTime,
        int periodicServiceInterval) => PrimitiveResult.Success(
            new Product
            {
                ProductCategoryId = ProductCategoryId,
                Price = price,
                Discount = discount,
                GuarantyExpirationTime = guarantyExpirationTime,
                PeriodicServiceInterval = periodicServiceInterval,
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
        int guarantyExpirationTime,
        int periodicServiceInterval)
    {
        this.ProductCategoryId = ProductCategoryId;
        this.Price = price;
        this.Discount = discount;
        this.GuarantyExpirationTime = guarantyExpirationTime;
        this.PeriodicServiceInterval = periodicServiceInterval;
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
        string langCode,
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords,
        int guarantyExpirationTime,
        int periodicServiceInterval,
        string imagePath,
        int? fileId)
    {
        return this.Update(ProductCategoryId, language, publishDate, publishInfo, publisher, price, discount, guarantyExpirationTime, periodicServiceInterval)
             .Map(_ => this.UpdateTranslation(langCode, title, description, @abstract, anchors, keywords, imagePath, fileId).Map(() => this));
    }

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

    public ValueTask<PrimitiveResult<Product>> UpdateStock(int quantity)
    {
        this.Stock += quantity;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }

    public async ValueTask<PrimitiveResult> AddMediaList(int[] mediaIds)
    {
        var maxOrdinal = (this._productMediaList?.Count ?? 0) == 0 ? 0 : this._productMediaList!.Max(a => a.Ordinal);

        await PrimitiveResult.BindAll(mediaIds, (mediaId, itemIndex) => ProductMedia.Create(
            this.Id,
            mediaId,
            maxOrdinal + itemIndex + 1),
            BindAllIterationStrategy.BreakOnFirstError)
        .OnSuccess(mediaList => this._productMediaList.AddRange(mediaList.Value));

        return PrimitiveResult.Success();
    }

    public PrimitiveResult DeleteMedia(int mediaId)
    {
        var x = _productMediaList.FirstOrDefault(s => s.MediaId == mediaId);
        if (x is not null)
        {
            _productMediaList.Remove(x);
        }
        return PrimitiveResult.Success();
    }

    public PrimitiveResult AddPeriodicService(int userId)
    {
        PeriodicService.Create(userId, this.Id, DateTime.Now.AddMonths(this.PeriodicServiceInterval))
            .OnSuccess(periodicService => this._periodicServices.Add(periodicService.Value));

        return PrimitiveResult.Success();
    }

    public PrimitiveResult DonePeriodicService()
    {
        var last = _periodicServices.OrderByDescending(s => s.CreationDate).First();
        last.DoneService()
              .Map(s => PeriodicService.Create(
                  last.UserId,
                  last.ProductId,
                  DateTime.Now.AddMonths(this.PeriodicServiceInterval))
              .OnSuccess(periodicService => this._periodicServices.Add(periodicService.Value)));
        return PrimitiveResult.Success();
    }
    #endregion
}
