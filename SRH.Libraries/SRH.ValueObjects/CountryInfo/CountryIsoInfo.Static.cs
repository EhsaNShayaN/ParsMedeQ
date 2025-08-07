using SRH.Utilities.Helpers;

namespace SRH.ValueObjects.CountryInfo;

public readonly partial record struct CountryIsoInfo
{
    public readonly static CountryIsoInfo Unknown = new("", string.Empty, string.Empty);

    private static Dictionary<string, CountryIsoInfo>? _iso2Countries = null;
    private static Dictionary<string, CountryIsoInfo>? _iso3Countries = null;

    private static readonly Dictionary<string, CountryIsoInfo> Iso2Countries = CreateIso2Countries();
    private static readonly Dictionary<string, CountryIsoInfo> Iso3Countries = CreateIso3Countries();

    private static void Generate()
    {
        var fieldsOfType = TypeHelpers.GetAllStaticFieldsOfType<CountryIsoInfo>().ToList();
        _iso2Countries = new Dictionary<string, CountryIsoInfo>(fieldsOfType.ToDictionary(x => x.Iso2, x => x), StringComparer.InvariantCultureIgnoreCase);
        _iso3Countries = new Dictionary<string, CountryIsoInfo>(fieldsOfType.ToDictionary(x => x.Iso3, x => x), StringComparer.InvariantCultureIgnoreCase);
    }
    private static Dictionary<string, CountryIsoInfo> CreateIso2Countries()
    {
        if (_iso2Countries is null) Generate();
        return _iso2Countries ?? new Dictionary<string, CountryIsoInfo>(StringComparer.InvariantCultureIgnoreCase);
    }
    private static Dictionary<string, CountryIsoInfo> CreateIso3Countries()
    {
        if (_iso3Countries is null) Generate();
        return _iso3Countries ?? new Dictionary<string, CountryIsoInfo>(StringComparer.InvariantCultureIgnoreCase);
    }

    public static IReadOnlyCollection<CountryIsoInfo> AllIso2Countries => Iso2Countries.Select(x => x.Value).ToList().AsReadOnly();
    public static IReadOnlyCollection<CountryIsoInfo> AllIso3Countries => Iso3Countries.Select(x => x.Value).ToList().AsReadOnly();
    public static CountryIsoInfo FromCode(string isoCode)
    {
        if (!HasValue(isoCode)) return CountryIsoInfo.Unknown;

        if (isoCode.Length == 2)
        {
            if (Iso2Countries.TryGetValue(isoCode, out var r)) return r;
            return CountryIsoInfo.Unknown;
        }

        if (isoCode.Length == 3)
        {
            if (Iso3Countries.TryGetValue(isoCode, out var r)) return r;
            return CountryIsoInfo.Unknown;
        }

        return CountryIsoInfo.Unknown;
    }

    private static bool HasValue(string? str) => str is not null
        && !string.IsNullOrWhiteSpace(str ?? string.Empty)
        && !string.IsNullOrEmpty(str ?? string.Empty);
}