using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.ServiceAggregate.Entities;

public sealed class ServiceTranslation : EntityBase<int>
{
    #region " Properties "
    public string LanguageCode { get; private set; } = string.Empty;
    public int ServiceId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    public Service Service { get; private set; } = null!;
    #endregion

    #region " Constructors "
    private ServiceTranslation() : base(0) { }
    public ServiceTranslation(int id) : base(id) { }
    #endregion

    #region " Factory "
    internal static PrimitiveResult<ServiceTranslation> Create(
        string languageCode,
        string title,
        string description,
        string image)
    {
        return PrimitiveResult.Success(
            new ServiceTranslation()
            {
                LanguageCode = languageCode,
                Title = title,
                Description = description,
                Image = image
            });
    }
    internal PrimitiveResult<ServiceTranslation> Update(
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