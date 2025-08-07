namespace SRH.PrimitiveTypes.Optional;

public static class OptionalExtension
{
    public static PrimitiveOption<T> ToOption<T>(this T? src) where T : class => PrimitiveOption<T>.From(src);

    public static PrimitiveOption<T> WhereOptin<T>(this T? src, Func<T, bool> predicate) where T : class
        => src is not null && predicate.Invoke(src!)
            ? PrimitiveOption<T>.Some(src!)
            : PrimitiveOption<T>.None;

    public static PrimitiveOption<T> WhereNotOption<T>(this T? src, Func<T, bool> predicate) where T : class
        => src is not null && !predicate.Invoke(src!)
            ? PrimitiveOption<T>.Some(src!)
            : PrimitiveOption<T>.None;

    public static PrimitiveValueOption<T> ToValueOption<T>(this T? src) where T : struct => PrimitiveValueOption<T>.From(src!.Value);

    public static PrimitiveValueOption<T> WhereValueOption<T>(this T? src, Func<T, bool> predicate) where T : struct
        => src is not null && predicate.Invoke(src!.Value)
            ? PrimitiveValueOption<T>.Some(src!.Value)
            : PrimitiveValueOption<T>.None;

    public static PrimitiveValueOption<T> WhereNotValueOption<T>(this T? src, Func<T, bool> predicate) where T : struct
        => src is not null && !predicate.Invoke(src!.Value)
            ? PrimitiveValueOption<T>.Some(src!.Value)
            : PrimitiveValueOption<T>.None;


    public static PrimitiveMaybe<T> ToMaybe<T>(this T? src) where T : struct => PrimitiveMaybe.From(src!.Value);

    public static PrimitiveMaybe<T> WhereMaybe<T>(this T? src, Func<T, bool> predicate)
       => src is not null && predicate.Invoke(src!)
           ? PrimitiveMaybe<T>.Some(src!)
           : PrimitiveMaybe<T>.None;

    public static PrimitiveMaybe<T> WhereNotMaybe<T>(this T? src, Func<T, bool> predicate)
        => src is not null && !predicate.Invoke(src!)
            ? PrimitiveMaybe<T>.Some(src!)
            : PrimitiveMaybe<T>.None;

    public async static ValueTask<T> GetOr<T>(this ValueTask<PrimitiveMaybe<T>> src, T defaultValue)
    {
        var valueTaskResult = await src.ConfigureAwait(false);
        return valueTaskResult.GetOr(defaultValue);
    }

    public async static ValueTask<T> GetOr<T>(this ValueTask<PrimitiveOption<T>> src, T defaultValue) where T : class
    {
        var valueTaskResult = await src.ConfigureAwait(false);
        return valueTaskResult.GetOr(defaultValue);
    }

    public async static ValueTask<T> GetOr<T>(this ValueTask<PrimitiveValueOption<T>> src, T defaultValue) where T : struct
    {
        var valueTaskResult = await src.ConfigureAwait(false);
        return valueTaskResult.GetOr(defaultValue);
    }

}