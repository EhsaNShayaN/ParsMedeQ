using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
public sealed class ResourceCategoryTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public ResourceCategory ResourceCategory { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ResourceCategoryTranslation() : base(0) { }
    public ResourceCategoryTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<ResourceCategoryTranslation> Create(
        string title,
        string description)
    {
        return PrimitiveResult.Success(
            new ResourceCategoryTranslation
            {
                Title = title,
                Description = description
            });
    }
    #endregion
}
