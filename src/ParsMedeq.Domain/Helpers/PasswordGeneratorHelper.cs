using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsMedeq.Domain.Helpers;
public static class PasswordGeneratorHelper
{

    readonly static char[] LowercaseChars = [];
    readonly static char[] UppercaseChars = [];
    readonly static char[] DigitChars = [];
    readonly static char[] SpecialChars = [];
    static PasswordGeneratorHelper()
    {
        LowercaseChars = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();
        UppercaseChars = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
        DigitChars = Enumerable.Range('0', 10).Select(x => (char)x).ToArray();
        SpecialChars = ['!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '|'];
    }
    public static string Generate(int length, PasswordGeneratorOptions opts = PasswordGeneratorOptions.Strong)
    {
        var result = new StringBuilder();

        char[] allChars = GetShuffledChars(opts);

        if (opts.HasFlag(PasswordGeneratorOptions.FirstLetterMustBeCharacter))
        {
            var letterChars = GetShuffledChars(PasswordGeneratorOptions.OnlyLetters);
            result.Append(Random.Shared.GetItems(letterChars, 1));
            length--;
        }
        result.Append(string.Join("", Random.Shared.GetItems(allChars, length)));
        return result.ToString();
    }

    static char[] GetShuffledChars(PasswordGeneratorOptions opts = PasswordGeneratorOptions.Strong)
    {
        char[] result = Enumerable.Empty<char>().Concat(ConcatByOption(opts, PasswordGeneratorOptions.UseLowercaseChars, LowercaseChars))
            .Concat(ConcatByOption(opts, PasswordGeneratorOptions.UseUppercaseChars, UppercaseChars))
            .Concat(ConcatByOption(opts, PasswordGeneratorOptions.UseDigits, DigitChars))
            .Concat(ConcatByOption(opts, PasswordGeneratorOptions.UseSpecialSymbols, SpecialChars))
            .ToArray();

        Random.Shared.Shuffle(result);

        return result;
    }

    static IEnumerable<char> ConcatByOption(PasswordGeneratorOptions opts, PasswordGeneratorOptions flag, char[] chars) =>
        opts.HasFlag(flag) ? chars : Enumerable.Empty<char>();



    [Flags]
    public enum PasswordGeneratorOptions
    {
        UseLowercaseChars = 1,
        UseUppercaseChars = 2,
        UseDigits = 4,
        UseSpecialSymbols = 8,
        FirstLetterMustBeCharacter = 16,
        Strong = UseLowercaseChars | UseUppercaseChars | UseDigits | UseSpecialSymbols | FirstLetterMustBeCharacter,
        OnlyLetters = UseLowercaseChars | UseUppercaseChars
    }

}
