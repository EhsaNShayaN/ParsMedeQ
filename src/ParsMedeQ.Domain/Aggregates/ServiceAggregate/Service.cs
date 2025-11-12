using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.ServiceAggregate.Entities;

namespace ParsMedeQ.Domain.Aggregates.ServiceAggregate;
public sealed class Service : EntityBase<int>
{
    #region " Fields "
    private List<ServiceTranslation> _serviceTranslations = [];
    #endregion

    #region " Properties "
    public byte TypeId { get; private set; }
    #endregion

    #region " Navigation Properties "
    public IReadOnlyCollection<ServiceTranslation> ServiceTranslations => this._serviceTranslations.AsReadOnly();
    #endregion

    #region " Constructors "
    private Service() : base(0) { }
    public Service(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Service> Create(
        byte typeId)
    {
        return PrimitiveResult.Success(
            new Service()
            {
                TypeId = typeId
            });
    }

    public ValueTask<PrimitiveResult> AddTranslation(
        string langCode,
        string title,
        string description,
        string image)
    {
        return ServiceTranslation.Create(langCode, title, description, image)
            .OnSuccess(newTranslation => this._serviceTranslations.Add(newTranslation.Value))
            .Match(
                success => PrimitiveResult.Success(),
                PrimitiveResult.Failure
            );
    }

    public ValueTask<PrimitiveResult<Service>> Update(
        string langCode,
        string title,
        string description,
        string image)
    {
        return this.UpdateTranslation(langCode, title, description, image).Map(() => this);
    }

    public ValueTask<PrimitiveResult> UpdateTranslation(
        string langCode,
        string title,
        string description,
        string image)
    {
        var currentTranslation = _serviceTranslations.FirstOrDefault(s => s.LanguageCode.Equals(langCode, StringComparison.OrdinalIgnoreCase));
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