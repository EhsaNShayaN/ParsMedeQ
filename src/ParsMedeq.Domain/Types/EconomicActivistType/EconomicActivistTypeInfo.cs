using EShop.Domain.Types.EconomicActivistDocument;

namespace EShop.Domain.Types.EconomicActivistType;

/// <summary>
/// نوع فعال اقتصادی: حقیقی - حقوقی - مشارکت مدنی - اتباع
/// </summary>
public readonly partial record struct EconomicActivistTypeInfo :
    ITaxPayerSystemType<byte>,
    IDbType<byte>
{

    public readonly byte Id { get; }
    public readonly string Name { get; }
    public readonly EconomicActivistDocumentInfo[] Documents { get; }

    private EconomicActivistTypeInfo(byte id, string name, EconomicActivistDocumentInfo[] documents)
    {
        this.Id = id;
        this.Name = name;
        this.Documents = documents;
    }
    public byte GetValue() => this.Id;
    public byte GetDbValue() => this.Id;
}

