using EShop.Domain.Abstractions;
using EShop.Domain.Aggregates.ProductAggregate;

namespace ParsMedeq.Domain.Aggregates.ResourceAggregate.Entities;
public sealed class ResourceCategory : EntityBase<int>
{
    #region " Fields "
    private List<Resource> _resources = [];
    private List<ResourceCategoryRelations> _resourceCategoryRelations = [];
    #endregion

    #region " Properties "
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public int TableId { get; private set; }
    public int Count { get; private set; }
    public int? ParentId { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public IReadOnlyCollection<Resource> Resources => this._resources.AsReadOnly();
    public IReadOnlyCollection<ResourceCategoryRelations> ResourceCategoryRelationss => this._resourceCategoryRelations.AsReadOnly();
    #endregion

    #region " Constructors "
    private ResourceCategory() : base(0) { }
    public ResourceCategory(int id) : base(id) { }
    #endregion
}