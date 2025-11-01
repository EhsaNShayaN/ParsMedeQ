using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.LocationAggregate.Entities;

public sealed class LocationTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = string.Empty;
    public int LocationId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public Location Location { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private LocationTranslation() : base(0) { }
    public LocationTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<LocationTranslation> Create(
        string languageCode,
        int LocationId,
        string title)
    {
        return PrimitiveResult.Success(
            new LocationTranslation()
            {
                LanguageCode = languageCode,
                LocationId = LocationId,
                Title = title
            });
    }
    #endregion
}
