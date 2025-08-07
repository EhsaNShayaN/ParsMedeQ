using EShop.Domain.Helpers;

namespace EShop.Domain.Types.VariantTypes;
/// <summary>
/// نام رنگ یا سایز
/// </summary>
public record VariantNameType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام رنگ یا سایز اشتباه است";

    public readonly static VariantNameType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private VariantNameType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<VariantNameType> Create(string value) => CreateUnsafe(value).Validate();

    public static VariantNameType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<VariantNameType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<VariantNameType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static VariantNameType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
