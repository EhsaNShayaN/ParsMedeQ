namespace ParsMedeQ.Domain.Types.EconomicActivistDocumentType;
public readonly partial record struct EconomicActivistDocumentTypeInfo
{
    public readonly byte Id { get; }
    public readonly string Name { get; }

    private EconomicActivistDocumentTypeInfo(byte id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

}
