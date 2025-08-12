using ParsMedeq.Domain.Types.FirstName;
using ParsMedeq.Domain.Types.LastName;

namespace ParsMedeq.Domain.Types.FullName;

/// <summary>
/// نام کامل
/// </summary>
public record FullNameType
{
    public readonly static FullNameType Empty = new FullNameType();

    public FirstNameType FirstName { get; private set; }
    public LastNameType LastName { get; private set; }

    public FullNameType() : this(FirstNameType.Empty, LastNameType.Empty) { }
    private FullNameType(FirstNameType firstName, LastNameType lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public string GetValue() => $"{this.FirstName.Value} {this.LastName.Value}".Trim();

    public static PrimitiveResult<FullNameType> Create(string firstname, string lastname)
    {
        var fname = FirstNameType.Create(firstname);
        var lname = LastNameType.Create(lastname);
        if (fname.IsFailure) return PrimitiveResult.Failure<FullNameType>(fname.Errors);
        if (lname.IsFailure) return PrimitiveResult.Failure<FullNameType>(lname.Errors);
        return new FullNameType(fname.Value, lname.Value);
    }

    public static FullNameType CreateUnsafe(string firstname, string lastname) =>
        new FullNameType(FirstNameType.CreateUnsafe(firstname), LastNameType.CreateUnsafe(lastname));

}