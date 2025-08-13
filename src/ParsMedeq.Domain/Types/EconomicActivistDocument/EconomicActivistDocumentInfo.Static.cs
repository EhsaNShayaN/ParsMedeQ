using ParsMedeQ.Domain.Types.EconomicActivistDocumentType;
using SRH.Utilities.Helpers;

namespace ParsMedeQ.Domain.Types.EconomicActivistDocument;
public readonly partial record struct EconomicActivistDocumentInfo
{

    public readonly static EconomicActivistDocumentInfo Unknown = new EconomicActivistDocumentInfo(0, string.Empty, EconomicActivistDocumentTypeInfo.Unknown);

    public readonly static EconomicActivistDocumentInfo BirthCertificate = new EconomicActivistDocumentInfo(1, "شناسنامه", EconomicActivistDocumentTypeInfo.PersonalIdentity);
    public readonly static EconomicActivistDocumentInfo NationalCard = new EconomicActivistDocumentInfo(2, "کارت ملی", EconomicActivistDocumentTypeInfo.PersonalIdentity);
    public readonly static EconomicActivistDocumentInfo OfficialNewspaper = new EconomicActivistDocumentInfo(3, "روزنامه رسمی", EconomicActivistDocumentTypeInfo.Legal);

    public readonly static EconomicActivistDocumentInfo CivilPartnership = new EconomicActivistDocumentInfo(4, "مدرک مربوط به مشارکت مدنی", EconomicActivistDocumentTypeInfo.Legal);

    public readonly static EconomicActivistDocumentInfo ForeignerPersonalIdentity = new EconomicActivistDocumentInfo(5, "مدرک  هویتی مربوط به اتباع", EconomicActivistDocumentTypeInfo.PersonalIdentity);
    public readonly static EconomicActivistDocumentInfo ForeignerLegalIdentity = new EconomicActivistDocumentInfo(6, "مدرک  شغلی مربوط به اتباع", EconomicActivistDocumentTypeInfo.Legal);

    public static EconomicActivistDocumentInfo FromId(byte id)
    {
        if (_allValues.TryGetValue(id, out var r)) return r;
        return Unknown;

    }
    public static IReadOnlyCollection<EconomicActivistDocumentInfo> AllValues => _allValues
        .Where(x => !x.Value.Equals(EconomicActivistDocumentInfo.Unknown))
        .Select(c => c.Value).ToList().AsReadOnly();

    private static Dictionary<byte, EconomicActivistDocumentInfo> _allValues = Generate();
    private static Dictionary<byte, EconomicActivistDocumentInfo> Generate()
    {
        var fieldsOfType = TypeHelpers.GetAllStaticFieldsOfType<EconomicActivistDocumentInfo>().ToList();
        return fieldsOfType.ToDictionary(x => x.Id, x => x);
    }
}