using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.SectionAggregate.Entities;
public sealed class SectionTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = string.Empty;
    public int SectionId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public Section Section { get; private set; }
    #endregion

    #region " Constructors "
    private SectionTranslation() : base(0) { }
    public SectionTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<SectionTranslation> Create(
        string languageCode,
        string title,
        string description,
        string image)
    {
        return PrimitiveResult.Success(
            new SectionTranslation()
            {
                LanguageCode = languageCode,
                Title = title,
                Description = description,
                Image = image
            });
    }
    internal PrimitiveResult<SectionTranslation> Update(
        string title,
        string description,
        string image)
    {
        this.Title = title;
        this.Description = description;
        this.Image = image;
        return this;
    }
    #endregion
}