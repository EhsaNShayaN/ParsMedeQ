namespace SRH.PrimitiveTypes.Maybe;

public readonly partial record struct PrimitiveMaybe<T>
{
    #region " Match "
    public static TOut Match<TOut>(PrimitiveMaybe<T> src,
        Func<T, TOut> onSuccess,
        Func<TOut> onFailrure) => src.HasValue ? onSuccess.Invoke(src.Value) : onFailrure.Invoke();

    public async static ValueTask<TOut> Match<TOut>(ValueTask<PrimitiveMaybe<T>> src,
        Func<T, TOut> onSuccess,
        Func<TOut> onFailrure)
    {
        var taskResult = await src.ConfigureAwait(false);
        return PrimitiveMaybe<T>.Match(
            taskResult,
            onSuccess,
            onFailrure);
    }

    public async static ValueTask<TOut> Match<TOut>(Task<PrimitiveMaybe<T>> src,
       Func<T, TOut> onSuccess,
       Func<TOut> onFailrure)
    {
        var taskResult = await src.ConfigureAwait(false);
        return PrimitiveMaybe<T>.Match(
            taskResult,
            onSuccess,
            onFailrure);
    }

    public static async ValueTask<TOut> Match<TOut>(PrimitiveMaybe<T> src,
        Func<T, ValueTask<TOut>> onSuccess,
        Func<TOut> onFailrure) =>
            src.HasValue
                ? await onSuccess.Invoke(src.Value).ConfigureAwait(false)
                : onFailrure.Invoke();

    public async static ValueTask<TOut> Match<TOut>(ValueTask<PrimitiveMaybe<T>> src,
        Func<T, ValueTask<TOut>> onSuccess,
        Func<TOut> onFailrure)
    {
        var taskResult = await src.ConfigureAwait(false);
        return await PrimitiveMaybe<T>.Match(
            taskResult,
            onSuccess,
            onFailrure)
        .ConfigureAwait(false);
    }

    public async static ValueTask<TOut> Match<TOut>(Task<PrimitiveMaybe<T>> src,
       Func<T, ValueTask<TOut>> onSuccess,
       Func<TOut> onFailrure)
    {
        var taskResult = await src.ConfigureAwait(false);
        return await PrimitiveMaybe<T>.Match(
            taskResult,
            onSuccess,
            onFailrure)
        .ConfigureAwait(false);
    }


    #endregion
}
