using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsMedeQ.Domain.Aggregates.ResourceAggregate;

public sealed class Resource : EntityBase<int>
{
    #region " Fields "
    private List<ResourceCategoryRelations> _resourceCategoryRelations = [];
    #endregion

    #region " Properties "
    public int TableId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Keywords { get; private set; } = string.Empty;
    public int ResourceCategoryId { get; private set; }
    public string ResourceCategoryTitle { get; set; } = string.Empty;
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
    #endregion

    #region " Constructors "
    private Resource() : base(0) { }
    public Resource(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Resource> Create(
        int tableId,
        string title,
        string @abstract,
        string anchors,
        string description,
        string keywords,
        int resourceCategoryId,
        string resourceCategoryTitle,
        string image,
        int? fileId,
        string language,
        string publishDate,
        string publishInfo,
        string publisher,
        int price,
        int discount,
        bool isVip,
        //int ordinal,
        DateTime? expirationDate)
    {
        return PrimitiveResult.Success(
            new Resource()
            {
                TableId = tableId,
                Title = title,
                Abstract = @abstract,
                Anchors = anchors,
                Description = description,
                Keywords = keywords,
                ResourceCategoryId = resourceCategoryId,
                ResourceCategoryTitle = resourceCategoryTitle,
                Image = image,
                FileId = fileId,
                Language = language,
                PublishDate = publishDate,
                PublishInfo = publishInfo,
                Publisher = publisher,
                Price = price,
                Discount = discount,
                IsVip = isVip,
                DownloadCount = 0,
                //Ordinal = ordinal,
                ExpirationDate = expirationDate,
                CreationDate = DateTime.Now
            });
    }
    #endregion

    public PrimitiveResult<Resource> SetFiles(string imagePath, int? fileId)
    {
        this.Image = imagePath ?? throw new ArgumentNullException("image not found");
        this.FileId = fileId;
        return this;
    }
}