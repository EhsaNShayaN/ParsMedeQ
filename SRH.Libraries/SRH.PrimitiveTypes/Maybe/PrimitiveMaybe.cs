namespace SRH.PrimitiveTypes.Maybe;

public readonly partial record struct PrimitiveMaybe
{
    public static PrimitiveMaybe<T> From<T>(T? value) => new(value);
    public async static ValueTask<PrimitiveMaybe<T>> From<T>(ValueTask<T?> value)
    {
        var result = await value.ConfigureAwait(false);
        return From(result);
    }
}
public readonly partial record struct PrimitiveMaybe<T>
{
    private readonly T? _value;

    public bool HasValue => _value is not null;
    public bool HasNotValue => !HasValue;
    public T Value => this.HasValue
        ? this._value!
        : throw new InvalidOperationException("The value can not be accessed because it does not exist.");


    internal PrimitiveMaybe(T? value) => this._value = value;


    public static PrimitiveMaybe<T> None => new(default);
    public static PrimitiveMaybe<T> From(T? value) => new(value);

    public static implicit operator PrimitiveMaybe<T>(T value) => PrimitiveMaybe.From(value);

    public static implicit operator T(PrimitiveMaybe<T> maybe) => maybe.Value;

    public bool Equals(PrimitiveMaybe<T> other)
    {
        if (this.HasNotValue && other.HasNotValue) return true;
        if (this.HasNotValue || other.HasNotValue) return false;
        return this.Value!.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return this.HasValue ? this.Value!.GetHashCode() : 0;
    }

}
