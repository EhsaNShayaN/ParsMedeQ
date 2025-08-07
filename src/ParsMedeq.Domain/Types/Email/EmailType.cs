using EShop.Domain.Helpers;

namespace EShop.Domain.Types.Email;
/// <summary>
/// ایمیل
/// </summary>
public record EmailType : IDbType<string>
{
    public const int MAX_LENGTH = 100;

    const string ERROR_MESSAGE = "ایمیل صحیح نمیباشد";

    public readonly static EmailType Empty = new EmailType(string.Empty);

    public string Value { get; private set; } = string.Empty;


    public EmailType() : this(string.Empty) { }
    private EmailType(string value) => Value = value;

    public static PrimitiveResult<EmailType> Create(string value) => CreateUnsafe(value).Validate();

    public static EmailType CreateUnsafe(string value) =>
        string.IsNullOrWhiteSpace(value)
        ? Empty
        : new EmailType((value ?? string.Empty).Trim());

    public PrimitiveResult<EmailType> Validate() =>
      IsValid(this.Value)
        ? this
        : DomainTypesHelper.CreateTypeErrorResult<EmailType>(ERROR_MESSAGE);

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetValue() => this.Value;
    public string GetDbValue() => Value;
    public static EmailType FromDb(string value)
    {
        if (value.Length > MAX_LENGTH) throw new InvalidDataException(ERROR_MESSAGE);
        var result = Create(value);
        return result.IsFailure
            ? Empty
            : result.Value;
    }

    private static bool IsValid(string? value)
    {
        value = value ?? string.Empty;
        value = value.Trim();

        if (string.IsNullOrWhiteSpace(value)) return true;

        if (value.Length > MAX_LENGTH) return false;

        var addr = new System.Net.Mail.MailAddress(value);

        return addr.Address == value;
    }
}