namespace ParsMedeQ.Domain.Helpers;
internal static class NationalIdHelper
{
    internal const int PersonalCode_Length = 10;
    internal const int ForeignerCode_Length = 12;

    readonly static Regex _regex_legal = new Regex(@"^(\d{11})$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    readonly static Regex _regex_civilpartnership = new Regex(@"^(\d{11})$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    readonly static Regex _regex_foreigner = new Regex(@"^(\d{12})$", RegexOptions.Compiled | RegexOptions.CultureInvariant);


    public static bool ValidateLegalId(string value)
    {
        int[] multiplier = [29, 27, 23, 19, 17];
        if (value.Length < 11) return false;
        if (!_regex_legal.IsMatch(value)) return false;
        var numbers = value.ToArray();
        var tenthNumberPlus2 = CharToToInt(numbers[9]) + 2;
        var sum = Enumerable.Range(0, 10)
            .Select(offset => (CharToToInt(numbers[offset]) + tenthNumberPlus2) * multiplier[offset % 5])
            .Sum();
        return CharToToInt(numbers[10]).Equals(sum % 11);
    }
    public static bool ValidatePersonalId(string value) => NationalCode.Validate(value).Match(() => true, _ => false);
    public static bool ValidateCivilPartnershipId(string value) => _regex_civilpartnership.IsMatch(value);
    public static bool ValidateForeignerId(string value) => _regex_foreigner.IsMatch(value);


    static int CharToToInt(char c) => Convert.ToInt32(c.ToString());
}
