namespace SRH.ValueObjects.CountryInfo;

public readonly partial record struct CountryIsoInfo
{
    public readonly string Name { get; }
    public readonly string Iso2 { get; }
    public readonly string Iso3 { get; }

    private CountryIsoInfo(string name, string iso2, string iso3)
    {
        Name = name;
        Iso2 = iso2;
        Iso3 = iso3;
    }

    public bool IsUnknown(CountryIsoInfo country) => country.Equals(Unknown);

}

