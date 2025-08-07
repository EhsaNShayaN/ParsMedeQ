namespace SRH.ValueObjects.Address;

public readonly partial record struct SimpleAddressInfo
{
    public readonly static SimpleAddressInfo Empty = new(string.Empty);

    public readonly string Value { get; }

    private SimpleAddressInfo(string value) => this.Value = value;

    public static SimpleAddressInfo Create(string value) => new(value);

    public bool IsEmpty() => Equals(Empty);
    public static bool IsEmpty(SimpleAddressInfo src) => src.IsEmpty();
}
