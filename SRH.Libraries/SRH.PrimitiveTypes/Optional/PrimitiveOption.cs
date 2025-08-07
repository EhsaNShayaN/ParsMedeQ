namespace SRH.PrimitiveTypes.Optional;
public readonly partial record struct PrimitiveOption<T> where T : class
{
    private readonly T? _value;

    internal PrimitiveOption(T? value) => this._value = value;
    internal bool IsNull() => this._value is null;
    internal bool IsNotNull() => this._value is not null;

    public readonly static PrimitiveOption<T> None = new(null);
    public static PrimitiveOption<T> Some(T value) => new(value);
    public static PrimitiveOption<T> From(T? value) => value is null ? PrimitiveOption<T>.None : Some(value);

    public PrimitiveOption<TResult> Map<TResult>(Func<T, TResult> mapper) where TResult : class =>
        this.IsNull() ? PrimitiveOption<TResult>.None : PrimitiveOption<TResult>.Some(mapper.Invoke(this._value!));
    public async ValueTask<PrimitiveOption<TResult>> Map<TResult>(Func<T, ValueTask<TResult>> mapper) where TResult : class =>
        this.IsNull() ? PrimitiveOption<TResult>.None : PrimitiveOption<TResult>.Some(await mapper.Invoke(this._value!).ConfigureAwait(false));

    public PrimitiveOption<TResult> MapOptional<TResult>(Func<T, PrimitiveOption<TResult>> mapper) where TResult : class =>
        this.IsNull() ? PrimitiveOption<TResult>.None : mapper.Invoke(this._value!);
    public async ValueTask<PrimitiveOption<TResult>> MapOptional<TResult>(Func<T, ValueTask<PrimitiveOption<TResult>>> mapper) where TResult : class =>
        this.IsNull() ? PrimitiveOption<TResult>.None : await mapper.Invoke(this._value!).ConfigureAwait(false);

    public PrimitiveValueOption<TResult> MapValue<TResult>(Func<T, TResult> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : PrimitiveValueOption<TResult>.Some(mapper.Invoke(this._value!));
    public async ValueTask<PrimitiveValueOption<TResult>> MapValue<TResult>(Func<T, ValueTask<TResult>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : PrimitiveValueOption<TResult>.Some(await mapper.Invoke(this._value!).ConfigureAwait(false));

    public PrimitiveValueOption<TResult> MapOptionalValue<TResult>(Func<T, PrimitiveValueOption<TResult>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : mapper.Invoke(this._value!);
    public async ValueTask<PrimitiveValueOption<TResult>> MapOptionalValue<TResult>(Func<T, ValueTask<PrimitiveValueOption<TResult>>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : await mapper.Invoke(this._value!).ConfigureAwait(false);


    public PrimitiveOption<T> Where(Func<T, bool> predicate) =>
        this.IsNotNull() && predicate.Invoke(this._value!) ? this : PrimitiveOption<T>.None;

    public PrimitiveOption<T> WhereNot(Func<T, bool> predicate) =>
        this.IsNotNull() && !predicate.Invoke(this._value!) ? this : PrimitiveOption<T>.None;

    public T GetOr(T defaultValue) => this.IsNull() ? defaultValue : this._value!;
    public T GetOr(Func<T> defaultValueGetter) => this.IsNull() ? defaultValueGetter.Invoke() : this._value!;
}
