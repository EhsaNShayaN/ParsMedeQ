using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.LocationAggregate;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
public sealed class TreatmentCenter : EntityBase<int>
{
    #region " Fields "
    private List<TreatmentCenterTranslation> _treatmentCenterTranslations = [];
    #endregion

    #region " Properties "
    public int ProvinceId { get; private set; }
    public int CityId { get; private set; }
    public DateTime CreationDate { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Location Province { get; private set; } = null!;
    public Location City { get; private set; } = null!;
    public IReadOnlyCollection<TreatmentCenterTranslation> TreatmentCenterTranslations => this._treatmentCenterTranslations.AsReadOnly();
    #endregion

    #region " Constructors "
    private TreatmentCenter() : base(0) { }
    public TreatmentCenter(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<TreatmentCenter> Create(
        int provinceId,
        int cityId) => PrimitiveResult.Success(
            new TreatmentCenter
            {
                ProvinceId = provinceId,
                CityId = cityId,
                CreationDate = DateTime.Now,
            });

    public ValueTask<PrimitiveResult<TreatmentCenter>> Update(
        int provinceId,
        int cityId)
    {
        this.CityId = provinceId;
        this.CityId = cityId;
        return ValueTask.FromResult(PrimitiveResult.Success(this));
    }

    public ValueTask<PrimitiveResult<TreatmentCenter>> Update(
        int provinceId,
        int cityId,
        string langCode,
        string title,
        string description,
        string image)
    {
        return this.Update(provinceId, cityId)
             .Map(_ => this.UpdateTranslation(langCode, title, description, image).Map(() => this));
    }

    public ValueTask<PrimitiveResult> AddTranslation(
        string langCode,
        string title,
        string description,
        string image)
    {
        return TreatmentCenterTranslation.Create(langCode, title, description, image)
            .OnSuccess(newTranslation => this._treatmentCenterTranslations.Add(newTranslation.Value))
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
        var currentTranslation = _treatmentCenterTranslations.FirstOrDefault(s => s.LanguageCode.Equals(langCode, StringComparison.OrdinalIgnoreCase));
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
    #endregion
}
