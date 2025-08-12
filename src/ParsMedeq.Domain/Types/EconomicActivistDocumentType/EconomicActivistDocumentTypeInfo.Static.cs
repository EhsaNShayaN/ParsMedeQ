using SRH.Utilities.Helpers;

namespace ParsMedeq.Domain.Types.EconomicActivistDocumentType;
public readonly partial record struct EconomicActivistDocumentTypeInfo
{
    public const int EconomicActivistDocumentType_Identity_Id = 1;
    public const int EconomicActivistDocumentType_Legal_Id = 2;


    public readonly static EconomicActivistDocumentTypeInfo Unknown = new EconomicActivistDocumentTypeInfo(0, string.Empty);

    public readonly static EconomicActivistDocumentTypeInfo PersonalIdentity = new EconomicActivistDocumentTypeInfo(EconomicActivistDocumentType_Identity_Id, "هویتی");
    public readonly static EconomicActivistDocumentTypeInfo Legal = new EconomicActivistDocumentTypeInfo(EconomicActivistDocumentType_Legal_Id, "شغلی");

    public static EconomicActivistDocumentTypeInfo FromId(byte id)
    {
        if (_allValues.TryGetValue(id, out var r)) return r;
        return Unknown;

    }
    public static IReadOnlyCollection<EconomicActivistDocumentTypeInfo> AllValues => _allValues
        .Where(x => !x.Value.Equals(EconomicActivistDocumentTypeInfo.Unknown))
        .Select(c => c.Value).ToList().AsReadOnly();

    private static Dictionary<byte, EconomicActivistDocumentTypeInfo> _allValues = Generate();
    private static Dictionary<byte, EconomicActivistDocumentTypeInfo> Generate()
    {
        var fieldsOfType = TypeHelpers.GetAllStaticFieldsOfType<EconomicActivistDocumentTypeInfo>().ToList();
        return fieldsOfType.ToDictionary(x => x.Id, x => x);
    }
}