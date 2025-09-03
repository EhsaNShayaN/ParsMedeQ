using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ResourceAggregate.Entities;
public sealed class ResourceTranslation : EntityBase<int>
{
    #region " Properties "
    public int ResourceId { get; private set; }
    public string LanguageCode { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public string Abstract { get; private set; } = string.Empty;
    public string Anchors { get; private set; } = string.Empty;
    public string Keywords { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public Resource Resource { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ResourceTranslation() : base(0) { }
    public ResourceTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<ResourceTranslation> Create(
        string languageCode,
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords) => PrimitiveResult.Success(
            new ResourceTranslation
            {
                LanguageCode = languageCode,
                Title = title,
                Description = description,
                Abstract = @abstract,
                Anchors = anchors,
                Keywords = keywords,
            });
    internal PrimitiveResult<ResourceTranslation> Update(
        string title,
        string description,
        string @abstract,
        string anchors,
        string keywords)
    {
        this.Title = title;
        this.Description = description;
        this.Abstract = @abstract;
        this.Anchors = anchors;
        this.Keywords = keywords;
        return this;
    }
    #endregion
}
