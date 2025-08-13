using ParsMedeQ.Domain.Types;

namespace ParsMedeQ.Domain.Types.VariantTypes;
/// <summary>
/// نوع متنوع کننده محصول
/// </summary>
public readonly partial record struct VariantTypeIdType :
    ITaxPayerSystemType<byte>,
    IDbType<byte>
{

    public readonly byte Id { get; }
    public readonly string Name { get; }

    private VariantTypeIdType(byte id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
    public byte GetValue() => this.Id;
    public byte GetDbValue() => this.Id;
}
