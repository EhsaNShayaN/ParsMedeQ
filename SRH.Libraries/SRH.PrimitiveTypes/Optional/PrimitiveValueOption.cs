namespace SRH.PrimitiveTypes.Optional;

public readonly partial record struct PrimitiveValueOption<T> where T : struct
{
    private readonly T? _value;

    private PrimitiveValueOption(T? value) => this._value = value;
    internal bool IsNotNull() => this._value.HasValue;
    internal bool IsNull() => !IsNotNull();

    public readonly static PrimitiveValueOption<T> None = new(null);
    public static PrimitiveValueOption<T> Some(T value) => new(value);
    public static PrimitiveValueOption<T> From(T? value) => value is null ? PrimitiveValueOption<T>.None : Some(value!.Value);

    public PrimitiveOption<TResult> Map<TResult>(Func<T, TResult> mapper) where TResult : class =>
         this.IsNull() ? PrimitiveOption<TResult>.None : PrimitiveOption<TResult>.Some(mapper.Invoke(this._value!.Value));
    public async ValueTask<PrimitiveOption<TResult>> Map<TResult>(Func<T, ValueTask<TResult>> mapper) where TResult : class =>
         this.IsNull() ? PrimitiveOption<TResult>.None : PrimitiveOption<TResult>.Some(await mapper.Invoke(this._value!.Value).ConfigureAwait(false));

    public PrimitiveOption<TResult> MapOptional<TResult>(Func<T, PrimitiveOption<TResult>> mapper) where TResult : class =>
        this.IsNull() ? PrimitiveOption<TResult>.None : mapper.Invoke(this._value!.Value);
    public async ValueTask<PrimitiveOption<TResult>> MapOptional<TResult>(Func<T, ValueTask<PrimitiveOption<TResult>>> mapper) where TResult : class =>
        this.IsNull() ? PrimitiveOption<TResult>.None : await mapper.Invoke(this._value!.Value).ConfigureAwait(false);

    public PrimitiveValueOption<TResult> MapValue<TResult>(Func<T, TResult> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : PrimitiveValueOption<TResult>.Some(mapper.Invoke(this._value!.Value));
    public async ValueTask<PrimitiveValueOption<TResult>> MapValue<TResult>(Func<T, ValueTask<TResult>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : PrimitiveValueOption<TResult>.Some(await mapper.Invoke(this._value!.Value).ConfigureAwait(false));

    public PrimitiveValueOption<TResult> MapOptionalValue<TResult>(Func<T, PrimitiveValueOption<TResult>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : mapper.Invoke(this._value!.Value);
    public async ValueTask<PrimitiveValueOption<TResult>> MapOptionalValue<TResult>(Func<T, ValueTask<PrimitiveValueOption<TResult>>> mapper) where TResult : struct =>
        this.IsNull() ? PrimitiveValueOption<TResult>.None : await mapper.Invoke(this._value!.Value).ConfigureAwait(false);


    public PrimitiveValueOption<T> Where(Func<T, bool> predicate) =>
        this.IsNotNull() && predicate.Invoke(this._value!.Value) ? this : PrimitiveValueOption<T>.None;

    public PrimitiveValueOption<T> WhereNot(Func<T, bool> predicate) =>
        this.IsNotNull() && !predicate.Invoke(this._value!.Value) ? this : PrimitiveValueOption<T>.None;

    public T GetOr(T defaultValue) => this.IsNull() ? defaultValue : this._value!.Value;
    public T GetOr(Func<T> defaultValueGetter) => this.IsNull() ? defaultValueGetter.Invoke() : this._value!.Value;
}
