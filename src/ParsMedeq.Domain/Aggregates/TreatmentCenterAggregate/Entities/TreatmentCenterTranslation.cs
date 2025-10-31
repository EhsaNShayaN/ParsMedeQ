using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate.Entities;

public sealed class TreatmentCenterTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = string.Empty;
    public int TreatmentCenterId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public TreatmentCenter TreatmentCenter { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private TreatmentCenterTranslation() : base(0) { }
    public TreatmentCenterTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<TreatmentCenterTranslation> Create(
        string languageCode,
        string title,
        string description,
        string image) => PrimitiveResult.Success(
            new TreatmentCenterTranslation
            {
                LanguageCode = languageCode,
                Title = title,
                Description = description,
                Image = image
            });
    internal PrimitiveResult<TreatmentCenterTranslation> Update(
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