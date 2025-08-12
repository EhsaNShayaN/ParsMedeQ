using ParsMedeq.Domain.Types.EconomicActivistDocument;
using SRH.Utilities.Helpers;

namespace ParsMedeq.Domain.Types.EconomicActivistType;

public readonly partial record struct EconomicActivistTypeInfo
{
    public const int TaxPayerType_Personal_Id = 1;
    public const int TaxPayerType_Legal_Id = 2;
    public const int TaxPayerType_CivilPartnership_Id = 3;
    public const int TaxPayerType_Foreigner_Id = 4;


    public readonly static EconomicActivistTypeInfo Unknown = new EconomicActivistTypeInfo(0, string.Empty, []);

    //public readonly static EconomicActivistTypeInfo Personal = new EconomicActivistTypeInfo(TaxPayerType_Personal_Id, "حقیقی", [EconomicActivistDocumentInfo.BirthCertificate, EconomicActivistDocumentInfo.NationalCard]);
    public readonly static EconomicActivistTypeInfo Personal = new EconomicActivistTypeInfo(TaxPayerType_Personal_Id, "حقیقی", []);
    public readonly static EconomicActivistTypeInfo Legal = new EconomicActivistTypeInfo(TaxPayerType_Legal_Id, "حقوقی", [EconomicActivistDocumentInfo.OfficialNewspaper]);
    public readonly static EconomicActivistTypeInfo CivilPartnership = new EconomicActivistTypeInfo(TaxPayerType_CivilPartnership_Id, "مشارکت مدنی", [EconomicActivistDocumentInfo.CivilPartnership]);
    public readonly static EconomicActivistTypeInfo Foreigner = new EconomicActivistTypeInfo(TaxPayerType_Foreigner_Id, "اتباع غیر ایرانی",
        [
        EconomicActivistDocumentInfo.ForeignerPersonalIdentity,
        EconomicActivistDocumentInfo.ForeignerLegalIdentity
        ]);

    public static EconomicActivistTypeInfo FromId(byte id)
    {
        if (_allValues.TryGetValue(id, out var r)) return r;
        return Unknown;

    }
    public static IReadOnlyCollection<EconomicActivistTypeInfo> AllValues => _allValues
        .Where(x => !x.Value.Equals(EconomicActivistTypeInfo.Unknown))
        .Select(c => c.Value).ToList().AsReadOnly();

    private static Dictionary<byte, EconomicActivistTypeInfo> _allValues = Generate();
    private static Dictionary<byte, EconomicActivistTypeInfo> Generate()
    {
        var fieldsOfType = TypeHelpers.GetAllStaticFieldsOfType<EconomicActivistTypeInfo>().ToList();
        return fieldsOfType.ToDictionary(x => x.Id, x => x);
    }
}
