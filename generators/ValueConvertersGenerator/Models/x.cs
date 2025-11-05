using ParsMedeQ.Domain.Abstractions;
using SRH.PrimitiveTypes.Result;

namespace ValueConvertersGenerator.Models;

public sealed class Service : EntityBase<int>
{
    #region " Properties "
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; }
    public string Image { get; private set; }
    #endregion

    #region " Navigation Properties "
    #endregion

    #region " Constructors "
    private Service() : base(0) { }
    public Service(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Service> Create(
        string title,
        string description,
        string image)
    {
        return PrimitiveResult.Success(
            new Service()
            {
                Title = title,
                Description = description,
                Image = image
            });
    }
    #endregion
}