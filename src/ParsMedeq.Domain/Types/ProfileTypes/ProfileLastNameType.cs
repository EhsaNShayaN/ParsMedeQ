using EShop.Domain.Helpers;

namespace EShop.Domain.Types.ProfileTypes;
/// <summary>
/// نام خانوادگی کاربر
/// </summary>
public record ProfileLastNameType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام کاربر اشتباه است";

    public readonly static ProfileLastNameType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private ProfileLastNameType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<ProfileLastNameType> Create(string value) => CreateUnsafe(value).Validate();

    public static ProfileLastNameType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<ProfileLastNameType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<ProfileLastNameType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static ProfileLastNameType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
