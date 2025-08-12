namespace ParsMedeq.Domain.Helpers;

public static class BranchCodeHelper
{
    readonly static Regex _regex = new Regex(@"^(\d{4})$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public const int MAX_BRANCH_CODE_LENGTH = 4;
    internal const string MAIN_BRANCH_CODE = "0000";
    internal const string MAIN_BRANCH_NAME = "مرکزی";


    public static bool IsValid(string code)
    {
        return string.IsNullOrWhiteSpace(code)
            || _regex.IsMatch(code);
    }
}