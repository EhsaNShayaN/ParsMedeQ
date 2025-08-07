using EShop.Domain.Helpers;

namespace EShop.Domain.Types.LastName;
/// <summary>
/// نام خانوادگی
/// </summary>
public record LastNameType : IDbType<string>
{
    public const int MAX_LENGTH = 50;

    const string ERROR_MESSAGE = "نام خانوادگی صحیح نمیباشد";

    public readonly static LastNameType Empty = new LastNameType(string.Empty);

    public string Value { get; private set; } = string.Empty;


    public LastNameType() : this(string.Empty) { }
    private LastNameType(string value) => Value = value;

    public static PrimitiveResult<LastNameType> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? Empty
            : new LastNameType(value.Trim()).Validate();
    }

    public static LastNameType CreateUnsafe(string value) => new LastNameType((value ?? string.Empty).Trim());

    public PrimitiveResult<LastNameType> Validate() =>
      string.IsNullOrWhiteSpace(this.Value)
           ? DomainTypesHelper.CreateTypeErrorResult<LastNameType>(ERROR_MESSAGE)
        : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetValue() => this.Value;
    public string GetDbValue() => Value;
    public static LastNameType FromDb(string value)
    {
        if (value.Length > MAX_LENGTH) throw new InvalidDataException(ERROR_MESSAGE);
        var result = Create(value);
        return result.IsFailure
            ? Empty
            : result.Value;
    }

    private static PrimitiveResult IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            return DomainTypesHelper.CreateTypeErrorResult(ERROR_MESSAGE);
        return PrimitiveResult.Success();
    }
}