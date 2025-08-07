namespace SRH.PrimitiveTypes.Optional;

public readonly partial record struct PrimitiveMaybe
{
    public static PrimitiveMaybe<T> From<T>(T? value) => value is null ? PrimitiveMaybe<T>.None : PrimitiveMaybe<T>.Some(value);
}
public readonly partial record struct PrimitiveMaybe<T>
{
    private readonly T? _value;

    private PrimitiveMaybe(T? value) => this._value = value;
    internal bool IsNull() => this._value is null || this._value.Equals(default(T));
    internal bool IsNotNull() => !this.IsNull();

    public readonly static PrimitiveMaybe<T> None = new(default);
    public static PrimitiveMaybe<T> Some(T value) => new(value);

    public PrimitiveMaybe<TResult> Map<TResult>(Func<T, TResult> mapper) =>
        this.IsNull() ? PrimitiveMaybe<TResult>.None : PrimitiveMaybe<TResult>.Some(mapper.Invoke(this._value!));

    public async ValueTask<PrimitiveMaybe<TResult>> Map<TResult>(Func<T, ValueTask<TResult>> mapper) =>
        this.IsNull() ? PrimitiveMaybe<TResult>.None : PrimitiveMaybe<TResult>.Some(await mapper.Invoke(this._value!).ConfigureAwait(false));

    public PrimitiveMaybe<TResult> MapOptional<TResult>(Func<T, PrimitiveMaybe<TResult>> mapper) =>
        this.IsNull() ? PrimitiveMaybe<TResult>.None : mapper.Invoke(this._value!);

    public async ValueTask<PrimitiveMaybe<TResult>> MapOptional<TResult>(Func<T, ValueTask<PrimitiveMaybe<TResult>>> mapper) =>
       this.IsNull() ? PrimitiveMaybe<TResult>.None : await mapper.Invoke(this._value!).ConfigureAwait(false);

    public PrimitiveValueOption<TResult> MapValue<TResult>(Func<T, TResult> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : PrimitiveValueOption<TResult>.Some(mapper.Invoke(this._value!));

    public async ValueTask<PrimitiveValueOption<TResult>> MapValue<TResult>(Func<T, ValueTask<TResult>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : PrimitiveValueOption<TResult>.Some(await mapper.Invoke(this._value!).ConfigureAwait(false));

    public PrimitiveValueOption<TResult> MapOptionalValue<TResult>(Func<T, PrimitiveValueOption<TResult>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : mapper.Invoke(this._value!);

    public async ValueTask<PrimitiveValueOption<TResult>> MapOptionalValue<TResult>(Func<T, ValueTask<PrimitiveValueOption<TResult>>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : await mapper.Invoke(this._value!).ConfigureAwait(false);


    public PrimitiveMaybe<T> Where(Func<T, bool> predicate) =>
        this.IsNotNull() && predicate.Invoke(this._value!) ? this : PrimitiveMaybe<T>.None;

    public PrimitiveMaybe<T> WhereNot(Func<T, bool> predicate) =>
        this.IsNotNull() && !predicate.Invoke(this._value!) ? this : PrimitiveMaybe<T>.None;

    public T GetOr(T defaultValue) => this.IsNull() ? defaultValue : this._value!;
    public T GetOr(Func<T> defaultValueGetter) => this.IsNull() ? defaultValueGetter.Invoke() : this._value!;
}
