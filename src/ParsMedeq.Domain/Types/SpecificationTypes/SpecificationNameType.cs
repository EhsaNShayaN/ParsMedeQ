using EShop.Domain.Helpers;

namespace EShop.Domain.Types.SpecificationTypes;
/// <summary>
/// نام مشخصه
/// </summary>
public record SpecificationNameType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام مشخصه اشتباه است";

    public readonly static SpecificationNameType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private SpecificationNameType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<SpecificationNameType> Create(string value) => CreateUnsafe(value).Validate();

    public static SpecificationNameType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<SpecificationNameType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<SpecificationNameType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static SpecificationNameType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
