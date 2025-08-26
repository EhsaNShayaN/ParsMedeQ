using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;
public sealed class ResourceCategoryTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int ResourceCategoryId { get; private set; }
    #endregion

    #region " Navigation Properties "
    public ResourceCategory ResourceCategory { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ResourceCategoryTranslation() : base(0) { }
    public ResourceCategoryTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<ResourceCategoryTranslation> Create(
        string languageCode,
        string title,
        string description)
    {
        return PrimitiveResult.Success(
            new ResourceCategoryTranslation
            {
                LanguageCode = languageCode,
                Title = title,
                Description = description
            });
    }
    #endregion
}
