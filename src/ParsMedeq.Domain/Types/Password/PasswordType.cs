namespace ParsMedeQ.Domain.Types.Password;
/// <summary>
/// کلمه عبور
/// </summary>
public record PasswordType
{
    public readonly static PasswordType Empty = new PasswordType();

    public string Value { get; private set; } = string.Empty;
    public string Salt { get; private set; }


    public PasswordType() : this(string.Empty, string.Empty) { }
    private PasswordType(string value, string salt) => (Value, Salt) = (value, salt);

    public static PrimitiveResult<PasswordType> Create(string value, string salt) => new PasswordType(value, salt);

    public static PasswordType CreateUnsafe(string value, string salt) => new PasswordType(value, salt);


    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);
}