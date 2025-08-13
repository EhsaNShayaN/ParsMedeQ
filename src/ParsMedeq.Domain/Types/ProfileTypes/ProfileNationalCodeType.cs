using ParsMedeQ.Domain.Types;

namespace ParsMedeQ.Domain.Types.ProfileTypes;
/// <summary>
/// نوع متنوع کننده محصول
/// </summary>
public readonly partial record struct ProfileNationalCodeType :
    ITaxPayerSystemType<long>,
    IDbType<long>
{

    public readonly long Id { get; }
    public readonly string Name { get; }

    private ProfileNationalCodeType(long id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
    public long GetValue() => this.Id;
    public long GetDbValue() => this.Id;
}
