using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.LocationAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;

namespace ParsMedeQ.Domain.Aggregates.LocationAggregate;

public sealed class Location : EntityBase<int>
{
    #region " Fields "
    private List<Location> _children = [];
    private List<LocationTranslation> _locationTranslations = [];
    private List<TreatmentCenter> _treatmentCenters = [];
    #endregion

    #region " Properties "
    public int? ParentId { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Location? Parent { get; private set; }
    public IReadOnlyCollection<Location> Children => this._children.AsReadOnly();
    public IReadOnlyCollection<LocationTranslation> LocationTranslations => this._locationTranslations.AsReadOnly();
    public IReadOnlyCollection<TreatmentCenter> TreatmentCenters => this._treatmentCenters.AsReadOnly();
    #endregion

    #region " Constructors "
    private Location() : base(0) { }
    public Location(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Location> Create(
        int parentId)
    {
        return PrimitiveResult.Success(
            new Location()
            {
                ParentId = parentId
            });
    }
    #endregion
}