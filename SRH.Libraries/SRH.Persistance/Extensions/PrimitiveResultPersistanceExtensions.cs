namespace SRH.Persistance.Extensions;

public static class PrimitiveResultPersistanceExtensions
{
    //public static PrimitiveResult<T> DefaultOf<T>() => PrimitiveResult.Success(default(T)!);

    //public static PrimitiveResult<TOut> MatchOnDefault<T, TOut>(this PrimitiveResult<T> src,
    //    Func<TOut> ifIsDefault,
    //    Func<T, TOut> ifIsNotDefault
    //    ) where T : class => src.Value is default(T)
    //        ? ifIsDefault.Invoke()
    //        : ifIsNotDefault.Invoke(src.Value);

    //public static async ValueTask<PrimitiveResult<TOut>> MatchOnDefault<T, TOut>(
    //    this ValueTask<PrimitiveResult<T>> src,
    //    Func<TOut> ifIsDefault,
    //    Func<T, TOut> ifIsNotDefault
    //    ) where T : class
    //{
    //    var result = await src.ConfigureAwait(false);
    //    return await src.MatchOnDefault(ifIsDefault, ifIsNotDefault)
    //        .ConfigureAwait(false);
    //}

    //public static async ValueTask<PrimitiveResult<TValue>> SaveToDatabase<TValue>(
    //    this ValueTask<PrimitiveResult<TValue>> src,
    //    IWriteUnitOfWork unitofWork,
    //    PrimitiveError error)
    //{
    //    var taskResult = await src.ConfigureAwait(false);
    //    return await taskResult.Bind(data =>
    //        unitofWork.SaveChangesAsync()
    //            .Ensure(rowsAffected => rowsAffected > 0, error)
    //            .Map(_ => data)
    //    ).ConfigureAwait(false);
    //}
}
