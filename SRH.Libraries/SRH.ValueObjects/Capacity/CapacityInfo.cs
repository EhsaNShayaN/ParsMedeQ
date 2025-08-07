namespace SRH.ValueObjects.Capacity;

public readonly record struct CapacityInfo
{
    public readonly static CapacityInfo Zero = new(0);

    public readonly int Value { get; }

    private CapacityInfo(int value) => this.Value = Math.Max(0, value);

    public bool IsZero() => this.Equals(Zero);

    public static CapacityInfo Create(int value) => new(value);

    public static bool IsZero(CapacityInfo src) => src.IsZero();
}
