using ParsMedeq.Domain.Abstractions;
using ParsMedeq.Domain.Aggregates.ProductAggregate;

namespace ParsMedeq.Domain.Aggregates.ResourceCategoryAggregate.Entities;
public sealed class ResourceCategoryRelations : EntityBase<int>
{
    #region " Properties "
    public int TableId { get; private set; }
    public int ResourceCategoryId { get; private set; }
    public int ResourceId { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public ResourceCategory ResourceCategory { get; private set; } = null!;
    public Resource Resource { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ResourceCategoryRelations() : base(0) { }
    public ResourceCategoryRelations(int id) : base(id) { }
    #endregion
}