namespace EShop.Domain.Helpers;

public static class StringHelper
{
    private static readonly Dictionary<char, int> CHARACTER_TO_NUMBER_CODING = new Dictionary<char, int>()
    {
        {'A', 65}, {'B', 66}, {'C', 67}, {'D', 68}, {'E', 69}, {'F', 70}, {'G', 71}, {'H', 72}, {'I', 73},
        {'J', 74}, {'K', 75}, {'L', 76}, {'M', 77}, {'N', 78}, {'O', 79}, {'P', 80}, {'Q', 81}, {'R', 82},
        {'S', 83}, {'T', 84}, {'U', 85}, {'V', 86}, {'W', 87}, {'X', 88}, {'Y', 89}, {'Z', 90},
    };

    //public static string ToDecimalString(string value)
    //{
    //    var decimalFormat = new StringBuilder();
    //    foreach (var ch in value)
    //    {
    //        if (char.IsDigit(ch))
    //        {
    //            decimalFormat.Append(ch);
    //        }
    //        else
    //        {
    //            decimalFormat.Append((int)ch);
    //        }
    //    }

    //    return decimalFormat.ToString();

    //}
    public static string ToDecimalString(string value) =>
        string.Join(string.Empty,
            value.ToUpperInvariant()
                .Select(c => char.IsDigit(c) ? c : CHARACTER_TO_NUMBER_CODING[c]));

    public static string ConvertToGeorgianNumber(this object Objstr, bool JustNumbers = false)
    {
        var str = Objstr?.ToString();
        if (string.IsNullOrEmpty(str)) return null;
        string ret = string.Empty;
        for (int i = 0; i < str.Length; i++)
        {
            int ascii = ConvertToASCII(str[i]);
            if (ascii >= 1776 && ascii <= 1787)
            {
                ret += ((char)(ascii - (1776 - 48))).ToString();
            }
            else if (ascii >= 1632 && ascii <= 1641)
            {
                ret += ((char)(ascii - (1632 - 48))).ToString();
            }
            else if (!JustNumbers || JustNumbers && ascii >= 48 && ascii <= 57)
            {
                ret += str[i];
            }
        }
        return ret;
    }
    public static int ConvertToASCII(char ch)
    {
        return ch;
    }
}