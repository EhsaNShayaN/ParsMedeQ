using EShop.Domain.Helpers;

namespace EShop.Domain.Types.FirstName;
/// <summary>
/// نام
/// </summary>
public record FirstNameType : IDbType<string>
{
    public const int MAX_LENGTH = 50;

    const string ERROR_MESSAGE = "نام صحیح نمیباشد";

    public readonly static FirstNameType Empty = new FirstNameType(string.Empty);

    public string Value { get; private set; } = string.Empty;


    public FirstNameType() : this(string.Empty) { }
    private FirstNameType(string value) => Value = value;

    public static PrimitiveResult<FirstNameType> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? Empty
            : new FirstNameType(value.Trim()).Validate();
    }

    public static FirstNameType CreateUnsafe(string value) => new FirstNameType((value ?? string.Empty).Trim());



    public PrimitiveResult<FirstNameType> Validate() =>
      IsValid(this.Value)
        ? this
        : DomainTypesHelper.CreateTypeErrorResult<FirstNameType>(ERROR_MESSAGE);

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetValue() => this.Value;
    public string GetDbValue() => Value;
    public static FirstNameType FromDb(string value)
    {
        if (value.Length > MAX_LENGTH) throw new InvalidDataException(ERROR_MESSAGE);
        var result = Create(value);
        return result.IsFailure
            ? Empty
            : result.Value;
    }

    private static bool IsValid(string? value)
    {
        return (!string.IsNullOrWhiteSpace(value) && value.Length <= MAX_LENGTH);
    }
}