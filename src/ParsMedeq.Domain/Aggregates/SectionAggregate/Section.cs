using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.SectionAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.SectionAggregate;
public sealed class Section : EntityBase<int>
{
    #region " Fields "
    private List<SectionTranslation> _sectionTranslations = [];
    #endregion

    #region " Properties "
    public bool Hidden { get; private set; }
    #endregion

    #region " Navigation Properties "
    public IReadOnlyCollection<SectionTranslation> SectionTranslations => this._sectionTranslations.AsReadOnly();
    #endregion

    #region " Constructors "
    private Section() : base(0) { }
    public Section(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Section> Create()
    {
        return PrimitiveResult.Success(
            new Section()
            {
                Hidden = true,
            });
    }
    private ValueTask<PrimitiveResult<Section>> Update()
    {
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }
    public ValueTask<PrimitiveResult<Section>> Update(
        string langCode,
        string title,
        string description,
        string image)
    {
        return this.Update()
             .Map(_ => this.UpdateTranslation(langCode, title, description, image).Map(() => this));
    }
    public ValueTask<PrimitiveResult> AddTranslation(
        string languageCode,
        string title,
        string description,
        string image)
    {
        return SectionTranslation.Create(
            languageCode,
            title,
            description,
            image)
            .OnSuccess(newTranslation => this._sectionTranslations.Add(newTranslation.Value))
            .Match(
                success => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }
    public ValueTask<PrimitiveResult> UpdateTranslation(
        string langCode,
        string title,
        string description,
        string image)
    {
        var currentTranslation = _sectionTranslations.FirstOrDefault(s => s.LanguageCode.Equals(langCode, StringComparison.OrdinalIgnoreCase));
        if (currentTranslation is null)
        {
            return this.AddTranslation(langCode, title, description, image);
        }
        return currentTranslation.Update(title, description, image)
            .Match(
                _ => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }

    public PrimitiveResult<Section> Show()
    {
        this.Hidden = false;
        return this;
    }

    public PrimitiveResult<Section> Hide()
    {
        this.Hidden = true;
        return this;
    }
    #endregion
}