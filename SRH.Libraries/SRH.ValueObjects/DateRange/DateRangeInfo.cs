namespace SRH.ValueObjects.DateRange;

public readonly record struct DateRangeInfo
{
    public readonly DateOnly Start { get; }
    public readonly DateOnly End { get; }

    private DateRangeInfo(DateOnly start, DateOnly end)
    {
        Start = start;
        End = end;
    }

    private static DateRangeInfo Create(DateOnly start, DateOnly end, bool validate)
    {
        if (validate)
        {
            if (start.Equals(DateOnly.MinValue) || start.Equals(DateOnly.MaxValue)) throw new InvalidOperationException("Start of StayRange can not be DateOnly.MinValue or DateOnly.MaxValue");
            if (end.Equals(DateOnly.MinValue) || end.Equals(DateOnly.MaxValue)) throw new InvalidOperationException("End of StayRange can not be DateOnly.MinValue or DateOnly.MaxValue");
        }

        return new DateRangeInfo(start, end);
    }

    public static DateRangeInfo Create(DateOnly start, DateOnly end) => Create(start, end, true);
    public static DateRangeInfo Create(DateTime start, DateTime end) => Create(DateOnly.FromDateTime(start), DateOnly.FromDateTime(end), true);
    public static DateRangeInfo Create(DateTimeOffset start, DateTimeOffset end) => Create(DateOnly.FromDateTime(start.DateTime), DateOnly.FromDateTime(end.DateTime), true);
    public static DateRangeInfo Create(string start, string end)
    {
        if (!DateOnly.TryParse(start, out var s)) throw new InvalidOperationException("Invalid date format");
        if (!DateOnly.TryParse(end, out var e)) throw new InvalidOperationException("Invalid date format");

        return Create(s, e, true);
    }

    public int GetDuration() => End.DayNumber - Start.DayNumber;
}

