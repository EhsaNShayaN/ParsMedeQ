using EShop.Domain.Helpers;

namespace EShop.Domain.Types.ProfileTypes;
/// <summary>
/// نام کاربر
/// </summary>
public record ProfileFirstNameType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام کاربر اشتباه است";

    public readonly static ProfileFirstNameType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private ProfileFirstNameType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<ProfileFirstNameType> Create(string value) => CreateUnsafe(value).Validate();

    public static ProfileFirstNameType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<ProfileFirstNameType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<ProfileFirstNameType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static ProfileFirstNameType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
