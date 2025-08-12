using ParsMedeq.Domain.Helpers;

namespace ParsMedeq.Domain.Types.ProfileTypes;
/// <summary>
/// موبایل کاربر
/// </summary>
public record ProfileMobileType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "موبایل کاربر اشتباه است";

    public readonly static ProfileMobileType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private ProfileMobileType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<ProfileMobileType> Create(string value) => CreateUnsafe(value).Validate();

    public static ProfileMobileType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<ProfileMobileType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<ProfileMobileType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static ProfileMobileType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
