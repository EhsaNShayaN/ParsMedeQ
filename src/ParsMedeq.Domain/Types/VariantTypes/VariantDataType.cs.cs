using EShop.Domain.Helpers;

namespace EShop.Domain.Types.VariantTypes;
/// <summary>
/// اطلاعات رنگ یا سایز
/// </summary>
public record VariantDataType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "عنوان برند اشتباه است";

    public readonly static VariantDataType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private VariantDataType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<VariantDataType> Create(string value) => CreateUnsafe(value).Validate();

    public static VariantDataType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<VariantDataType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<VariantDataType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static VariantDataType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
