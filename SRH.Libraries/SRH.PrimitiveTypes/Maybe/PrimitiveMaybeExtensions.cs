namespace SRH.PrimitiveTypes.Maybe;


public static partial class PrimitiveMaybeExtensions
{
    public static TOut Match<T, TOut>(this PrimitiveMaybe<T> src,
         Func<T, TOut> onSuccess,
         Func<TOut> onFailrure) => PrimitiveMaybe<T>.Match(src, onSuccess, onFailrure);

    public static ValueTask<TOut> Match<T, TOut>(this ValueTask<PrimitiveMaybe<T>> src,
         Func<T, TOut> onSuccess,
         Func<TOut> onFailrure) => PrimitiveMaybe<T>.Match(src, onSuccess, onFailrure);

    public static ValueTask<TOut> Match<T, TOut>(this Task<PrimitiveMaybe<T>> src,
      Func<T, TOut> onSuccess,
      Func<TOut> onFailrure) => PrimitiveMaybe<T>.Match(src, onSuccess, onFailrure);

    public static ValueTask<TOut> Match<T, TOut>(this PrimitiveMaybe<T> src,
        Func<T, ValueTask<TOut>> onSuccess,
        Func<TOut> onFailrure) => PrimitiveMaybe<T>.Match(src, onSuccess, onFailrure);

    public static ValueTask<TOut> Match<T, TOut>(this ValueTask<PrimitiveMaybe<T>> src,
        Func<T, ValueTask<TOut>> onSuccess,
        Func<TOut> onFailrure) => PrimitiveMaybe<T>.Match(src, onSuccess, onFailrure);

    public static ValueTask<TOut> Match<T, TOut>(this Task<PrimitiveMaybe<T>> src,
       Func<T, ValueTask<TOut>> onSuccess,
       Func<TOut> onFailrure) => PrimitiveMaybe<T>.Match(src, onSuccess, onFailrure);


    public static TOut DoOrFailure<T, TOut, TException>(this PrimitiveMaybe<T> src,
      Func<T, TOut> onSuccess,
      TException exception) where TException : Exception => src.HasValue
           ? onSuccess.Invoke(src.Value)
           : throw exception;

}