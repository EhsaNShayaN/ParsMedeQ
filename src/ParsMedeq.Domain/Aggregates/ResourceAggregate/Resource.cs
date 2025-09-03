using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsMedeQ.Domain.Aggregates.ResourceAggregate;

public sealed class Resource : EntityBase<int>
{
    #region " Fields "
    private List<ResourceCategoryRelations> _resourceCategoryRelations = [];
    private List<ResourceTranslation> _resourceTranslations = [];
    #endregion

    #region " Properties "
    public int TableId { get; private set; }
    public int ResourceCategoryId { get; private set; }
    public string Image { get; private set; } = string.Empty;
    public int? FileId { get; private set; }
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
    public ResourceCategory? ResourceCategory { get; private set; }
    [NotMapped]
    public ResourceCategory[]? ResourceCategories { get; set; }
    public IReadOnlyCollection<ResourceCategoryRelations> ResourceCategoryRelations => this._resourceCategoryRelations.AsReadOnly();
    public IReadOnlyCollection<ResourceTranslation> ResourceTranslations => this._resourceTranslations.AsReadOnly();
    #endregion

    #region " Constructors "
    private Resource() : base(0) { }
    public Resource(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Resource> Create(
        int tableId,
        int resourceCategoryId,
        string language,
        string publishDate,
        string publishInfo,
        string publisher,
        int price,
        int discount,
        bool isVip,
        DateTime? expirationDate) => PrimitiveResult.Success(
            new Resource
            {
                TableId = tableId,
                ResourceCategoryId = resourceCategoryId,
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

    public ValueTask<PrimitiveResult<Resource>> Update(
        int resourceCategoryId,
        string language,
        string publishDate,
        string publishInfo,
        string publisher,
        int price,
        int discount,
        bool isVip,
        DateTime? expirationDate)
    {
        this.ResourceCategoryId = resourceCategoryId;
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

    public ValueTask<PrimitiveResult<Resource>> Update(
        int resourceCategoryId,
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
        string keywords)
    {
        return this.Update(resourceCategoryId, language, publishDate, publishInfo, publisher, price, discount, isVip, expirationDate)
             .Map(_ => this.UpdateTranslation(langCode, title, description, @abstract, anchors, keywords).Map(() => this));
    }
    #endregion

    public PrimitiveResult<Resource> SetFiles(string imagePath, int? fileId)
    {
        this.Image = imagePath ?? throw new ArgumentNullException("image not found");
        this.FileId = fileId;
        return this;
    }

    public ValueTask<PrimitiveResult> AddTranslation(string langCode, string title, string description, string @abstract, string anchors, string keywords)
    {
        return ResourceTranslation.Create(langCode, title, description, @abstract, anchors, keywords)
            .OnSuccess(newTranslation => this._resourceTranslations.Add(newTranslation.Value))
            .Match(
                success => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }

    public ValueTask<PrimitiveResult> UpdateTranslation(string langCode, string title, string description, string @abstract, string anchors, string keywords)
    {
        var currentTranslation = _resourceTranslations.FirstOrDefault(s => s.LanguageCode.Equals(langCode, StringComparison.OrdinalIgnoreCase));
        if (currentTranslation is null)
        {
            return this.AddTranslation(langCode, title, description, @abstract, anchors, keywords);
        }
        return currentTranslation.Update(title, description, @abstract, anchors, keywords)
            .Match(
                _ => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }
}