using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ResourceAggregate.Entities;
public sealed class ResourceTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public Resource Resource { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ResourceTranslation() : base(0) { }
    public ResourceTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<ResourceTranslation> Create(
        string title,
        string description,
        string @abstract,
        string anchors)
    {
        return PrimitiveResult.Success(
            new ResourceTranslation()
            {
                Title = title,
                Description = description,
                Abstract = @abstract,
                Anchors = anchors,
            });
    }
    #endregion
}
