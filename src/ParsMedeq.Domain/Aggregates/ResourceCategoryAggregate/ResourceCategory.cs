using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
public sealed class ResourceCategory : EntityBase<int>
{
    #region " Fields "
    private List<Resource> _resources = [];
    private List<ResourceCategory> _resourceCategories = [];
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
    public ResourceCategory Parent { get; private set; }
    public IReadOnlyCollection<Resource> Resources => this._resources.AsReadOnly();
    public IReadOnlyCollection<ResourceCategoryRelations> ResourceCategoryRelations => this._resourceCategoryRelations.AsReadOnly();
    public IReadOnlyCollection<ResourceCategory> Children => this._resourceCategories.AsReadOnly();
    #endregion

    #region " Constructors "
    private ResourceCategory() : base(0) { }
    public ResourceCategory(int id) : base(id) { }
    #endregion
}