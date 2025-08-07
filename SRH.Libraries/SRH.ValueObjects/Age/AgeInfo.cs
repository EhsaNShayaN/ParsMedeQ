
namespace SRH.ValueObjects.Age;

public readonly record struct AgeInfo
{
    public readonly int Years { get; }
    public readonly int Months { get; }
    public readonly int Days { get; }

    private AgeInfo(int years, int months, int days)
    {
        Years = years;
        Months = months;
        Days = days;
    }


    public static ValueTask<PrimitiveResult<AgeInfo>> Create(BirthdateInfo birthdate, DateOnly from)
    {
        CalcAccurateAge(birthdate.Value, from, out int y, out int m, out int d);

        if (y < 0 || m < 0 || d < 0) return ValueTask.FromResult(PrimitiveResult.Failure<AgeInfo>(ValueObjectErrors.BirthdateValueError));

        return ValueTask.FromResult(PrimitiveResult.Success(new AgeInfo(y, m, d)));
    }

    private static void CalcAccurateAge(DateOnly birthday, DateOnly from, out int years, out int months, out int days)
    {
        months = from.Month - birthday.Month;
        years = from.Year - birthday.Year;

        if (from.Day < birthday.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }
        days = from.DayNumber - birthday.AddMonths(years * 12 + months).DayNumber;
    }
}
