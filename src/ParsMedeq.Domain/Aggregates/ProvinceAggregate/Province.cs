using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ProvinceAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;

namespace ParsMedeQ.Domain.Aggregates.ProvinceAggregate;

public sealed class Province : EntityBase<int>
{
    #region " Fields "
    private List<Province> _cities = [];
    private List<ProvinceTranslation> _provinceTranslations = [];
    private List<TreatmentCenter> _treatmentCenters = [];
    #endregion

    #region " Properties "
    public int? ParentId { get; private set; }
    #endregion

    #region " Navigation Properties "
    public Province? Parent { get; private set; }
    public IReadOnlyCollection<Province> Cities => this._cities.AsReadOnly();
    public IReadOnlyCollection<ProvinceTranslation> ProvinceTranslations => this._provinceTranslations.AsReadOnly();
    public IReadOnlyCollection<TreatmentCenter> TreatmentCenters => this._treatmentCenters.AsReadOnly();
    #endregion

    #region " Constructors "
    private Province() : base(0) { }
    public Province(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Province> Create(
        int parentId)
    {
        return PrimitiveResult.Success(
            new Province()
            {
                ParentId = parentId
            });
    }
    #endregion
}