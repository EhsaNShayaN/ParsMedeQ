using System.Globalization;

namespace SRH.ValueObjects.DateRange.ShamsiDateRange;
public readonly record struct ShamsiDateRangeInfo
{
    private readonly static CultureInfo ENCulture = CultureInfo.GetCultureInfo("en-US");
    private readonly static CultureInfo FACulture = CultureInfo.GetCultureInfo("fa-IR");

    public readonly string Start { get; }
    public readonly string End { get; }
    public readonly DateOnly GregorianStart { get; }
    public readonly DateOnly GregorianEnd { get; }

    private ShamsiDateRangeInfo(string start, string end)
    {
        this.Start = start;
        this.End = end;

        this.GregorianStart = DateOnly.Parse(start, FACulture);
        this.GregorianEnd = DateOnly.Parse(end, FACulture);

    }

    private static ShamsiDateRangeInfo Create(string start, string end, bool validate)
    {
        if (validate)
        {
            if (string.IsNullOrWhiteSpace(start)) throw new InvalidOperationException("Start of ShamsiStayRange can not be empty");
            if (string.IsNullOrWhiteSpace(end)) throw new InvalidOperationException("End of ShamsiStayRange can not be empty");
        }

        var result = new ShamsiDateRangeInfo(start, end);

        if (result.GetDuration() < 0) throw new InvalidOperationException("End of ShamsiStayRange can not be less than Start of it.");

        return result;
    }
    public static ShamsiDateRangeInfo Create(string start, string end) => Create(start, end, true);
    public static ShamsiDateRangeInfo CreateWithoutValidation(string start, string end) => Create(start, end, false);


    public int GetDuration() => GregorianEnd.DayNumber - GregorianStart.DayNumber;

    public static ValueTask<PrimitiveResult> IsValid(string start, string end)
    {
        if (string.IsNullOrWhiteSpace(start)) return ValueTask.FromResult(PrimitiveResult.Failure("", "Start of ShamsiDateRange can not be empty"));
        if (string.IsNullOrWhiteSpace(end)) return ValueTask.FromResult(PrimitiveResult.Failure("", "End of ShamsiDateRange can not be empty"));

        var gregorianStart = DateOnly.Parse(start, FACulture);
        var gregorianEnd = DateOnly.Parse(end, FACulture);

        if (gregorianEnd.DayNumber - gregorianStart.DayNumber < 0) return ValueTask.FromResult(PrimitiveResult.Failure("", "End of ShamsiStayRange can not be less than Start of it"));

        return ValueTask.FromResult(PrimitiveResult.Success());
    }
}

