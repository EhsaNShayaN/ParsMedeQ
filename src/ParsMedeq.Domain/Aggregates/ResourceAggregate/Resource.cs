using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.ProductAggregate;

public sealed class Resource : EntityBase<int>
{
    #region " Fields "
    private List<ResourceCategoryRelations> _resourceCategoryRelations = [];
    #endregion

    #region " Properties "
    public string Title { get; private set; } = null!;
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Keywords { get; private set; } = string.Empty;

    public int SecondId { get; private set; }
    public int TableId { get; private set; }
    public int? CategoryId { get; private set; }
    public string CategoryTitle { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public string MimeType { get; private set; } = string.Empty;
    public string Doc { get; private set; } = string.Empty;
    public int? JournalId { get; private set; }
    public string Language { get; private set; } = string.Empty;
    public string PublishDate { get; private set; } = string.Empty;
    public string PublishInfo { get; private set; } = string.Empty;
    public string Publisher { get; private set; } = string.Empty;
    public int? Price { get; private set; }
    public int? Discount { get; private set; }
    public bool IsVip { get; private set; }
    public bool Pinned { get; private set; }
    public int DownloadCount { get; private set; }
    public int? Ordinal { get; private set; }
    public bool ShowInChem { get; private set; }
    public bool ShowInAcademy { get; private set; }
    public bool Deleted { get; private set; }
    public bool Disabled { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public ResourceCategory ResourceCategory { get; private set; } = null!;
    public IReadOnlyCollection<ResourceCategoryRelations> ResourceCategoryRelations => this._resourceCategoryRelations.AsReadOnly();
    #endregion

    #region " Constructors "
    private Resource() : base(0) { }
    public Resource(int id) : base(id) { }
    #endregion
}