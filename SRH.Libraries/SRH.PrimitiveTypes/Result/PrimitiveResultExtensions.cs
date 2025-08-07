namespace SRH.PrimitiveTypes.Result;
public static partial class PrimitiveResultExtensions
{
    #region " Ensure "
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, bool> predicate, PrimitiveError error) =>
        PrimitiveResult.Ensure(src, predicate, error);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, ValueTask<bool>> predicate, PrimitiveError error) =>
    PrimitiveResult.Ensure(src, predicate, error);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult> predicate) => PrimitiveResult.Ensure(src, predicate);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult> predicate, PrimitiveError error) => PrimitiveResult.Ensure(src, predicate, error);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TValue>> predicate) => PrimitiveResult.Ensure(src, predicate);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TValue>> predicate, PrimitiveError error) => PrimitiveResult.Ensure(src, predicate, error);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult>> predicate) => PrimitiveResult.Ensure(src, predicate);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult>> predicate, PrimitiveError error) => PrimitiveResult.Ensure(src, predicate, error);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> predicate) => PrimitiveResult.Ensure(src, predicate);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> predicate, PrimitiveError error) => PrimitiveResult.Ensure(src, predicate, error);


    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, bool> predicate, PrimitiveError error) =>
        PrimitiveResult.Ensure(src, predicate, error);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<bool>> predicate, PrimitiveError error) =>
        PrimitiveResult.Ensure(src, predicate, error);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params (Func<TValue, bool> predicate, PrimitiveError error)[] functions) =>
        PrimitiveResult.Ensure(src, functions);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params (Func<TValue, ValueTask<bool>> predicate, PrimitiveError error)[] functions) =>
        PrimitiveResult.Ensure(src, functions);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this ValueTask<PrimitiveResult<TValue>> src, params (Func<TValue, bool> predicate, PrimitiveError error)[] functions) =>
        PrimitiveResult.Ensure(src, functions);

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this ValueTask<PrimitiveResult<TValue>> src, params (Func<TValue, ValueTask<bool>> predicate, PrimitiveError error)[] functions) =>
        PrimitiveResult.Ensure(src, functions);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params Func<TValue, PrimitiveResult>[] predicates) => PrimitiveResult.Ensure(src, predicates);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params Func<TValue, ValueTask<PrimitiveResult>>[] predicates) => PrimitiveResult.Ensure(src, predicates);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params Func<PrimitiveResult>[] predicates) => PrimitiveResult.Ensure(src, predicates);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params Func<ValueTask<PrimitiveResult>>[] predicates) => PrimitiveResult.Ensure(src, predicates);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params Func<PrimitiveResult<TValue>>[] predicates) => PrimitiveResult.Ensure(src, predicates);
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(this PrimitiveResult<TValue> src, params Func<ValueTask<PrimitiveResult<TValue>>>[] predicates) => PrimitiveResult.Ensure(src, predicates);
    #endregion

    #region " Map "

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(this PrimitiveResult src, Func<TOut> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(this PrimitiveResult src, Func<PrimitiveResult<TOut>> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(this PrimitiveResult src, Func<ValueTask<PrimitiveResult<TOut>>> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(this ValueTask<PrimitiveResult> src, Func<TOut> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(this ValueTask<PrimitiveResult> src, Func<PrimitiveResult<TOut>> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(this ValueTask<PrimitiveResult> src, Func<ValueTask<PrimitiveResult<TOut>>> mapper) => PrimitiveResult.Map(src, mapper);


    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(this PrimitiveResult<TValue> src, Func<TValue, TOut> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(this PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TOut>> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(this PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TOut>>> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, TOut> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult<TOut>> mapper) => PrimitiveResult.Map(src, mapper);

    /// <summary>
    /// Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="src">The result.</param>
    /// <param name="mapper">The mapping function.</param>
    /// <returns>
    /// The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult<TOut>>> mapper) => PrimitiveResult.Map(src, mapper);
    #endregion

    #region " MapIf "
    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrueMapper,
        Func<TValue, TOut> ifFalseMapper) => PrimitiveResult.MapIf(src, predicate, ifTrueMapper, ifFalseMapper);

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TOut>> ifTrueMapper,
        Func<TValue, PrimitiveResult<TOut>> ifFalseMapper) => PrimitiveResult.MapIf(src, predicate, ifTrueMapper, ifFalseMapper);

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifTrueMapper,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifFalseMapper) => PrimitiveResult.MapIf(src, predicate, ifTrueMapper, ifFalseMapper);

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrueMapper,
        Func<TValue, TOut> ifFalseMapper) => PrimitiveResult.MapIf(src, predicate, ifTrueMapper, ifFalseMapper);

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TOut>> ifTrueMapper,
        Func<TValue, PrimitiveResult<TOut>> ifFalseMapper) => PrimitiveResult.MapIf(src, predicate, ifTrueMapper, ifFalseMapper);

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifTrueMapper,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifFalseMapper) => PrimitiveResult.MapIf(src, predicate, ifTrueMapper, ifFalseMapper);
    #endregion

    #region " Bind "
    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(this PrimitiveResult src, Func<PrimitiveResult, PrimitiveResult> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(this PrimitiveResult src, Func<PrimitiveResult, ValueTask<PrimitiveResult>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TValue>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this PrimitiveResult<TValue> src, Func<TValue, Task<TValue>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this PrimitiveResult<TValue> src, Func<TValue, Task<PrimitiveResult<TValue>>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(this ValueTask<PrimitiveResult> src, Func<PrimitiveResult, PrimitiveResult> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(this ValueTask<PrimitiveResult> src, Func<PrimitiveResult, ValueTask<PrimitiveResult>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult<TValue>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, Task<TValue>> func) => PrimitiveResult.Bind(src, func);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) => PrimitiveResult.Bind(src, func);

    #endregion

    #region " BindIf "
    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TValue>> func) => PrimitiveResult.BindIf(src, predicate, func);

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TValue>> func) => PrimitiveResult.BindIf(src, predicate, func);

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) => PrimitiveResult.BindIf(src, predicate, func);


    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(this PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TValue>>> func) => PrimitiveResult.BindIf(src, predicate, func);

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TValue>> func) => PrimitiveResult.BindIf(src, predicate, func);

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TValue>> func) => PrimitiveResult.BindIf(src, predicate, func);

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(this ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) => PrimitiveResult.BindIf(src, predicate, func);

    #endregion

    #region " Match "
    public static ValueTask<TOut> Match<TValue, TOut>(
        this PrimitiveResult<TValue> src,
        Func<TValue, TOut> onSuccess,
        Func<PrimitiveError[], TOut> onFailure) => PrimitiveResult.Match(src, onSuccess, onFailure);

    public static ValueTask<TOut> Match<TValue, TOut>(
       this ValueTask<PrimitiveResult<TValue>> src,
       Func<TValue, TOut> onSuccess,
       Func<PrimitiveError[], TOut> onFailure) => PrimitiveResult.Match(src, onSuccess, onFailure);

    public static ValueTask<TOut> Match<TValue, TOut>(
        this Task<PrimitiveResult<TValue>> src,
        Func<TValue, TOut> onSuccess,
        Func<PrimitiveError[], TOut> onFailure) => PrimitiveResult.Match(src, onSuccess, onFailure);

    public static async ValueTask<TOut> Match<TValue, TOut>(
       this ValueTask<PrimitiveResult<TValue>> src,
       Func<TValue, Task<TOut>> onSuccess,
       Func<PrimitiveError[], TOut> onFailure)
    {
        var taskResult = await src.ConfigureAwait(false);

        if (taskResult.IsSuccess)
        {
            return await onSuccess.Invoke(taskResult.Value).ConfigureAwait(false);
        }

        return onFailure.Invoke(taskResult.Errors);
    }

    public static async ValueTask<TOut> Match<TValue, TOut>(
       this Task<PrimitiveResult<TValue>> src,
       Func<TValue, Task<TOut>> onSuccess,
       Func<PrimitiveError[], TOut> onFailure)
    {
        var taskResult = await src.ConfigureAwait(false);

        if (taskResult.IsSuccess)
        {
            return await onSuccess.Invoke(taskResult.Value).ConfigureAwait(false);
        }

        return onFailure.Invoke(taskResult.Errors);
    }

    public static TOut Match<TOut>(
         this PrimitiveResult src,
         Func<TOut> onSuccess,
         Func<PrimitiveError[], TOut> onFailure) => PrimitiveResult.Match(src, onSuccess, onFailure);

    public static ValueTask<TOut> Match<TOut>(
        this ValueTask<PrimitiveResult> src,
        Func<TOut> onSuccess,
        Func<PrimitiveError[], TOut> onFailure) => PrimitiveResult.Match(src, onSuccess, onFailure);
    #endregion

    public async static ValueTask<PrimitiveResult> ToPrimitiveResult<TValue>(this ValueTask<PrimitiveResult<TValue>> src)
    {
        var taskResult = await src.ConfigureAwait(false);
        return taskResult.IsSuccess
            ? PrimitiveResult.Success()
            : PrimitiveResult.Failure(taskResult.Errors);
    }

    #region " Do "
    public static ValueTask<PrimitiveResult<TValue>> Do<TValue>(this PrimitiveResult<TValue> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.Do(src, act);
    public static ValueTask<PrimitiveResult<TValue>> Do<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.Do(src, act);
    public static ValueTask<PrimitiveResult<TValue>> Do<TValue>(this Task<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.Do(src, act);
    public static ValueTask<PrimitiveResult<TValue>> OnSuccess<TValue>(this PrimitiveResult<TValue> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.OnSuccess(src, act);
    public static ValueTask<PrimitiveResult<TValue>> OnSuccess<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.OnSuccess(src, act);
    public static ValueTask<PrimitiveResult<TValue>> OnSuccess<TValue>(this Task<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.OnSuccess(src, act);
    public static ValueTask<PrimitiveResult<TValue>> OnFailure<TValue>(this PrimitiveResult<TValue> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.OnFailure(src, act);
    public static ValueTask<PrimitiveResult<TValue>> OnFailure<TValue>(this ValueTask<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.OnFailure(src, act);
    public static ValueTask<PrimitiveResult<TValue>> OnFailure<TValue>(this Task<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.OnFailure(src, act);

    #endregion
}

