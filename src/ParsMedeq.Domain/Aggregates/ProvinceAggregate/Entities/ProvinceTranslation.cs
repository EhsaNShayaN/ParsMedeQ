using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ProvinceAggregate.Entities;

public sealed class ProvinceTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = string.Empty;
    public int ProvinceId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public Province Province { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ProvinceTranslation() : base(0) { }
    public ProvinceTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<ProvinceTranslation> Create(
        string languageCode,
        int provinceId,
        string title)
    {
        return PrimitiveResult.Success(
            new ProvinceTranslation()
            {
                LanguageCode = languageCode,
                ProvinceId = provinceId,
                Title = title
            });
    }
    #endregion
}
