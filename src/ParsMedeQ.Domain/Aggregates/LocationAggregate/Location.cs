using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.LocationAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;

namespace ParsMedeQ.Domain.Aggregates.LocationAggregate;

public sealed class Location : EntityBase<int>
{
    #region " Fields "
    private List<Location> _children = [];
    private List<LocationTranslation> _locationTranslation = [];
    private List<TreatmentCenter> _treatmentCenter = [];
    #endregion

    #region " Properties "
    public int? ParentId { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Location? Parent { get; private set; }
    public IReadOnlyCollection<Location> Children => this._children.AsReadOnly();
    public IReadOnlyCollection<LocationTranslation> LocationTranslation => this._locationTranslation.AsReadOnly();
    public IReadOnlyCollection<TreatmentCenter> TreatmentCenter => this._treatmentCenter.AsReadOnly();
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