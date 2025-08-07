using EShop.Domain.Types.EconomicActivistDocumentType;

namespace EShop.Domain.Types.EconomicActivistDocument;
public readonly partial record struct EconomicActivistDocumentInfo : IDbType<byte>
{
    public readonly byte Id { get; }
    public readonly string Name { get; }
    public EconomicActivistDocumentTypeInfo Type { get; }

    private EconomicActivistDocumentInfo(byte id, string name, EconomicActivistDocumentTypeInfo type)
    {
        this.Id = id;
        this.Name = name;
        this.Type = type;
    }

    public byte GetDbValue() => this.Id;
    public static EconomicActivistDocumentInfo FromDb(byte value) => EconomicActivistDocumentInfo.FromId(value);
}
