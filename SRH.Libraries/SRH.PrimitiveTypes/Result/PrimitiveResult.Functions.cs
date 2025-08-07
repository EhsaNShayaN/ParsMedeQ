namespace SRH.PrimitiveTypes.Result;
public readonly partial record struct PrimitiveResult
{
    #region " Ensure "
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, bool> predicate, PrimitiveError error)
    {
        if (src.IsFailure) return ValueTask.FromResult(src);

        return ValueTask.FromResult(
            predicate(src.Value)
                ? src
                : Failure<TValue>(error));
    }
    public async static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, ValueTask<bool>> predicate, PrimitiveError error)
    {
        if (src.IsFailure) return src;

        var ensureResult = await predicate.Invoke(src.Value).ConfigureAwait(false);

        return ensureResult ? src : Failure<TValue>(error);
    }

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult> predicate)
    {
        if (src.IsFailure) return ValueTask.FromResult(src);

        var predicateResult = predicate.Invoke(src.Value);

        return ValueTask.FromResult(predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(predicateResult)
            : src);
    }
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult> predicate, PrimitiveError error)
    {
        if (src.IsFailure) return ValueTask.FromResult(src);

        var predicateResult = predicate.Invoke(src.Value);

        return ValueTask.FromResult(predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(error)
            : src);
    }
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TValue>> predicate)
    {
        if (src.IsFailure) return ValueTask.FromResult(src);

        var predicateResult = predicate.Invoke(src.Value);

        return ValueTask.FromResult(predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(predicateResult)
            : src);
    }
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TValue>> predicate, PrimitiveError error)
    {
        if (src.IsFailure) return ValueTask.FromResult(src);

        var predicateResult = predicate.Invoke(src.Value);

        return ValueTask.FromResult(predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(error)
            : src);
    }

    public async static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult>> predicate)
    {
        if (src.IsFailure) return src;

        var predicateResult = await predicate.Invoke(src.Value).ConfigureAwait(false);

        return predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(predicateResult)
            : src;
    }
    public async static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult>> predicate, PrimitiveError error)
    {
        if (src.IsFailure) return src;

        var predicateResult = await predicate.Invoke(src.Value).ConfigureAwait(false);

        return predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(error)
            : src;
    }
    public async static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> predicate)
    {
        if (src.IsFailure) return src;

        var predicateResult = await predicate.Invoke(src.Value).ConfigureAwait(false);

        return predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(predicateResult)
            : src;
    }
    public async static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> predicate, PrimitiveError error)
    {
        if (src.IsFailure) return src;

        var predicateResult = await predicate.Invoke(src.Value).ConfigureAwait(false);

        return predicateResult.IsFailure
            ? PrimitiveResult.Failure<TValue>(error)
            : src;
    }


    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, bool> predicate, PrimitiveError error) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate, error));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<bool>> predicate, PrimitiveError error) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate, error));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult> predicate) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult> predicate, PrimitiveError error) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate, error));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult<TValue>> predicate) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult<TValue>> predicate, PrimitiveError error) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate, error));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult>> predicate) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult>> predicate, PrimitiveError error) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate, error));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> predicate) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> predicate, PrimitiveError error) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, predicate, error));


    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, (Func<TValue, bool> predicate, PrimitiveError error)[] functions) =>
        Combine(functions.Select(f => Ensure(src, f.predicate, f.error)).ToArray());
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, (Func<TValue, ValueTask<bool>> predicate, PrimitiveError error)[] functions) =>
        Combine(functions.Select(f => Ensure(src, f.predicate, f.error)).ToArray());
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, (Func<TValue, bool> predicate, PrimitiveError error)[] functions) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, functions));
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(ValueTask<PrimitiveResult<TValue>> src, (Func<TValue, ValueTask<bool>> predicate, PrimitiveError error)[] functions) =>
        PrimitiveResult._BindValueTaskCore<TValue, TValue>(src, (taskResult) => Ensure(taskResult, functions));

    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, params Func<TValue, PrimitiveResult>[] predicates)
    {
        if (src.IsFailure) return ValueTask.FromResult(PrimitiveResult.Failure<TValue>(src));
        var errors = new List<PrimitiveError>();

        for (int i = 0; i < predicates.Length; i++)
        {
            var predicateResult = predicates[i].Invoke(src.Value);
            if (predicateResult.IsFailure) errors.AddRange(predicateResult.Errors);
        }

        if (errors.Any()) return ValueTask.FromResult(PrimitiveResult.Failure<TValue>(errors.ToArray()));

        return ValueTask.FromResult(src);
    }
    public static async ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, params Func<TValue, ValueTask<PrimitiveResult>>[] predicates)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TValue>(src);
        var errors = new List<PrimitiveError>();

        for (int i = 0; i < predicates.Length; i++)
        {
            var predicateResult = await predicates[i].Invoke(src.Value).ConfigureAwait(false);
            if (predicateResult.IsFailure) errors.AddRange(predicateResult.Errors);
        }

        if (errors.Any()) return PrimitiveResult.Failure<TValue>(errors.ToArray());

        return src;
    }
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, params Func<PrimitiveResult>[] predicates)
    {
        if (src.IsFailure) return ValueTask.FromResult(PrimitiveResult.Failure<TValue>(src));
        var errors = new List<PrimitiveError>();

        for (int i = 0; i < predicates.Length; i++)
        {
            var predicateResult = predicates[i].Invoke();
            if (predicateResult.IsFailure) errors.AddRange(predicateResult.Errors);
        }

        if (errors.Any()) return ValueTask.FromResult(PrimitiveResult.Failure<TValue>(errors.ToArray()));

        return ValueTask.FromResult(src);
    }
    public static async ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, params Func<ValueTask<PrimitiveResult>>[] predicates)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TValue>(src);
        var errors = new List<PrimitiveError>();

        for (int i = 0; i < predicates.Length; i++)
        {
            var predicateResult = await predicates[i].Invoke().ConfigureAwait(false);
            if (predicateResult.IsFailure) errors.AddRange(predicateResult.Errors);
        }

        if (errors.Any()) return PrimitiveResult.Failure<TValue>(errors.ToArray());

        return src;
    }
    public static ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, params Func<PrimitiveResult<TValue>>[] predicates)
    {
        if (src.IsFailure) return ValueTask.FromResult(PrimitiveResult.Failure<TValue>(src));
        var errors = new List<PrimitiveError>();

        for (int i = 0; i < predicates.Length; i++)
        {
            var predicateResult = predicates[i].Invoke();
            if (predicateResult.IsFailure) errors.AddRange(predicateResult.Errors);
        }

        if (errors.Any()) return ValueTask.FromResult(PrimitiveResult.Failure<TValue>(errors.ToArray()));

        return ValueTask.FromResult(src);
    }
    public static async ValueTask<PrimitiveResult<TValue>> Ensure<TValue>(PrimitiveResult<TValue> src, params Func<ValueTask<PrimitiveResult<TValue>>>[] predicates)
    {
        if (src.IsFailure) return PrimitiveResult.Failure<TValue>(src);
        var errors = new List<PrimitiveError>();

        for (int i = 0; i < predicates.Length; i++)
        {
            var predicateResult = await predicates[i].Invoke().ConfigureAwait(false);
            if (predicateResult.IsFailure) errors.AddRange(predicateResult.Errors);
        }

        if (errors.Any()) return PrimitiveResult.Failure<TValue>(errors.ToArray());

        return src;
    }


    #endregion

    #region " Combine "

    public static ValueTask<PrimitiveResult<TValue>> Combine<TValue>(params PrimitiveResult<TValue>[] results)
    {
        var failrureResults = results.Where(x => x.IsFailure).ToArray();

        return ValueTask.FromResult(
            failrureResults.Any()
            ? Failure<TValue>(failrureResults.SelectMany(failrureResult => failrureResult.Errors).Distinct().ToArray())
            : results[0]);
    }
    public static async ValueTask<PrimitiveResult<TValue>> Combine<TValue>(params ValueTask<PrimitiveResult<TValue>>[] src)
    {
        List<PrimitiveResult<TValue>> list = new(src.Length);
        foreach (var f in src)
        {
            list.Add(await f.ConfigureAwait(false));
        }
        return await Combine(list.ToArray()).ConfigureAwait(false);
    }
    public static ValueTask<PrimitiveResult<IEnumerable<TValue>>> CombineAll<TValue>(params PrimitiveResult<TValue>[] results)
    {
        var failrureResults = results.Where(x => x.IsFailure).ToArray();

        return ValueTask.FromResult(
                failrureResults.Any()
                ? Failure<IEnumerable<TValue>>(failrureResults.SelectMany(failrureResult => failrureResult.Errors).Distinct().ToArray())
                : PrimitiveResult.Success<IEnumerable<TValue>>(results.Select(successResult => successResult.Value)));
    }
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
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(PrimitiveResult src, Func<TOut> mapper) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure<TOut>(src.Errors)
                : PrimitiveResult.Success(mapper()));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(PrimitiveResult src, Func<PrimitiveResult<TOut>> mapper) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure<TOut>(src.Errors)
                : mapper());

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(PrimitiveResult src, Func<ValueTask<PrimitiveResult<TOut>>> mapper) =>
            src.IsFailure
                ? ValueTask.FromResult(PrimitiveResult.Failure<TOut>(src.Errors))
                : mapper();

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(ValueTask<PrimitiveResult> src, Func<TOut> mapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.Map(taskResult, mapper));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(ValueTask<PrimitiveResult> src, Func<PrimitiveResult<TOut>> mapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.Map(taskResult, mapper));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(ValueTask<PrimitiveResult> src, Func<ValueTask<PrimitiveResult<TOut>>> mapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.Map(taskResult, mapper));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(PrimitiveResult<TValue> src, Func<TValue, TOut> mapper) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure<TOut>(src.Errors)
                : PrimitiveResult.Success(mapper(src.Value)));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TOut>> mapper) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure<TOut>(src.Errors)
                : mapper(src.Value));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TOut>>> mapper) =>
        src.IsFailure
            ? ValueTask.FromResult(PrimitiveResult.Failure<TOut>(src.Errors))
            : mapper(src.Value);

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, TOut> mapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.Map(taskResult, mapper));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult<TOut>> mapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.Map(taskResult, mapper));

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
    public static ValueTask<PrimitiveResult<TOut>> Map<TValue, TOut>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult<TOut>>> mapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.Map(taskResult, mapper));
    #endregion

    #region " MapIf "
    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrueMapper,
        Func<TValue, TOut> ifFalseMapper) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure<TOut>(src.Errors)
                : predicate(src.Value)
                    ? PrimitiveResult.Success(ifTrueMapper(src.Value))
                    : PrimitiveResult.Success(ifFalseMapper(src.Value)));

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TOut>> ifTrueMapper,
        Func<TValue, PrimitiveResult<TOut>> ifFalseMapper) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure<TOut>(src.Errors)
                : predicate(src.Value)
                    ? ifTrueMapper(src.Value)
                    : ifFalseMapper(src.Value));

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifTrueMapper,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifFalseMapper) =>
            src.IsFailure
                ? ValueTask.FromResult(PrimitiveResult.Failure<TOut>(src.Errors))
                 : predicate(src.Value)
                    ? ifTrueMapper(src.Value)
                    : ifFalseMapper(src.Value);

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, TOut> ifTrueMapper,
        Func<TValue, TOut> ifFalseMapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.MapIf(taskResult, predicate, ifTrueMapper, ifFalseMapper));

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TOut>> ifTrueMapper,
        Func<TValue, PrimitiveResult<TOut>> ifFalseMapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.MapIf(taskResult, predicate, ifTrueMapper, ifFalseMapper));

    public static ValueTask<PrimitiveResult<TOut>> MapIf<TValue, TOut>(ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifTrueMapper,
        Func<TValue, ValueTask<PrimitiveResult<TOut>>> ifFalseMapper) => _BindValueTaskCore(src, taskResult => PrimitiveResult.MapIf(taskResult, predicate, ifTrueMapper, ifFalseMapper));
    #endregion

    #region " Bind "
    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(PrimitiveResult src, Func<PrimitiveResult, PrimitiveResult> func) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure(src)
                : func(src));

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(PrimitiveResult src, Func<PrimitiveResult, ValueTask<PrimitiveResult>> func) =>
           src.IsFailure
               ? ValueTask.FromResult(PrimitiveResult.Failure(src))
               : func(src);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(PrimitiveResult<TValue> src, Func<TValue, PrimitiveResult<TValue>> func) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure(src)
                : func(src.Value));

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public async static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(PrimitiveResult<TValue> src, Func<TValue, Task<TValue>> func)
    {
        if (src.IsFailure) return src;

        var taskResult = await func(src.Value).ConfigureAwait(false);

        if (taskResult is null) return PrimitiveResult.Failure<TValue>("Error", "Task result is null");

        return PrimitiveResult.Success<TValue>(taskResult);
    }

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(PrimitiveResult<TValue> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) =>
        src.IsFailure
            ? ValueTask.FromResult(PrimitiveResult.Failure(src))
            : func(src.Value);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static async ValueTask<PrimitiveResult<TValue>> Bind<TValue>(PrimitiveResult<TValue> src, Func<TValue, Task<PrimitiveResult<TValue>>> func) =>
            src.IsFailure
                ? PrimitiveResult.Failure(src)
                : await func(src.Value).ConfigureAwait(false);

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(ValueTask<PrimitiveResult> src, Func<PrimitiveResult, PrimitiveResult> func) => _BindValueTaskCore(src, taskResult => Bind(taskResult, func));

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult> Bind(ValueTask<PrimitiveResult> src, Func<PrimitiveResult, ValueTask<PrimitiveResult>> func) => _BindValueTaskCore(src, taskResult => Bind(taskResult, func));

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, PrimitiveResult<TValue>> func) => _BindValueTaskCore(src, taskResult => Bind(taskResult, func));

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, Task<TValue>> func) => _BindValueTaskCore(src, taskResult => Bind(taskResult, func));

    /// <summary>
    /// Binds to the result of the function and returns it.
    /// </summary>
    /// <param name="src">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>The success result with the bound value if the current result is a success result, otherwise a failure result.</returns>
    public static ValueTask<PrimitiveResult<TValue>> Bind<TValue>(ValueTask<PrimitiveResult<TValue>> src, Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) => _BindValueTaskCore(src, taskResult => Bind(taskResult, func));

    #endregion

    #region " BindIf "
    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TValue>> func) =>
        ValueTask.FromResult(
            src.IsFailure
                ? PrimitiveResult.Failure(src)
                : predicate(src.Value) ? func(src.Value) : src);


    public async static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TValue>> func)
    {
        if (src.IsFailure) return src;

        if (!predicate(src.Value)) return src;

        var taskResult = await func(src.Value).ConfigureAwait(false);

        if (taskResult is null) return PrimitiveResult.Failure<TValue>("Error", "Task result is null");

        return PrimitiveResult.Success<TValue>(taskResult);
    }

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) =>
        src.IsFailure
            ? ValueTask.FromResult(PrimitiveResult.Failure(src))
            : predicate(src.Value) ? func(src.Value) : ValueTask.FromResult(src);


    public static async ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(PrimitiveResult<TValue> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<PrimitiveResult<TValue>>> func) =>
            src.IsFailure
                ? PrimitiveResult.Failure(src)
                : predicate(src.Value) ? await func(src.Value).ConfigureAwait(false) : src;

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, PrimitiveResult<TValue>> func) => _BindValueTaskCore(src, taskResult => BindIf(taskResult, predicate, func));

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, Task<TValue>> func) => _BindValueTaskCore(src, taskResult => BindIf(taskResult, predicate, func));

    public static ValueTask<PrimitiveResult<TValue>> BindIf<TValue>(ValueTask<PrimitiveResult<TValue>> src,
        Func<TValue, bool> predicate,
        Func<TValue, ValueTask<PrimitiveResult<TValue>>> func) => _BindValueTaskCore(src, taskResult => BindIf(taskResult, predicate, func));

    #endregion

    #region " BindAll "
    public static async ValueTask<PrimitiveResult<IEnumerable<TOut>>> BindAll<TIn, TOut>(IEnumerable<TIn> list, Func<TIn, PrimitiveResult<TOut>> func, BindAllIterationStrategy iterationStrategy = BindAllIterationStrategy.BreakOnFirstError)
    {
        var listCount = list.Count();
        var resultList = new List<PrimitiveResult<TOut>>();
        for (var i = 0; i < listCount; i++)
        {
            var currentFuncResult = await func.Invoke(list.ElementAt(i))
                .Do(resultList.Add)
                .ConfigureAwait(false);

            if (currentFuncResult.IsFailure && iterationStrategy.HasFlag(BindAllIterationStrategy.BreakOnFirstError)) break;

        }

        return await PrimitiveResult.CombineAll(resultList.ToArray()).ConfigureAwait(false);
    }
    public static async ValueTask<PrimitiveResult<IEnumerable<TOut>>> BindAll<TIn, TOut>(IEnumerable<TIn> list, Func<TIn, int, PrimitiveResult<TOut>> func, BindAllIterationStrategy iterationStrategy = BindAllIterationStrategy.BreakOnFirstError)
    {
        var listCount = list.Count();
        var resultList = new List<PrimitiveResult<TOut>>();
        for (var i = 0; i < listCount; i++)
        {
            var currentFuncResult = await func.Invoke(list.ElementAt(i), i)
                .Do(resultList.Add)
                .ConfigureAwait(false);

            if (currentFuncResult.IsFailure && iterationStrategy.HasFlag(BindAllIterationStrategy.BreakOnFirstError)) break;
        }

        return await PrimitiveResult.CombineAll(resultList.ToArray()).ConfigureAwait(false);
    }
    public static async ValueTask<PrimitiveResult<IEnumerable<TOut>>> BindAll<TIn, TOut>(IEnumerable<TIn> list, Func<TIn, ValueTask<PrimitiveResult<TOut>>> func, BindAllIterationStrategy iterationStrategy = BindAllIterationStrategy.BreakOnFirstError)
    {
        var listCount = list.Count();
        var resultList = new List<PrimitiveResult<TOut>>();
        for (var i = 0; i < listCount; i++)
        {
            var currentFuncResult = await func.Invoke(list.ElementAt(i))
                .Do(resultList.Add)
                .ConfigureAwait(false);

            if (currentFuncResult.IsFailure && iterationStrategy.HasFlag(BindAllIterationStrategy.BreakOnFirstError)) break;
        }

        return await PrimitiveResult.CombineAll(resultList.ToArray()).ConfigureAwait(false);
    }
    public static async ValueTask<PrimitiveResult<IEnumerable<TOut>>> BindAll<TIn, TOut>(IEnumerable<TIn> list, Func<TIn, int, ValueTask<PrimitiveResult<TOut>>> func, BindAllIterationStrategy iterationStrategy = BindAllIterationStrategy.BreakOnFirstError)
    {
        var listCount = list.Count();
        var resultList = new List<PrimitiveResult<TOut>>();
        for (var i = 0; i < listCount; i++)
        {
            var currentFuncResult = await func.Invoke(list.ElementAt(i), i)
                .Do(resultList.Add)
                .ConfigureAwait(false);

            if (currentFuncResult.IsFailure && iterationStrategy.HasFlag(BindAllIterationStrategy.BreakOnFirstError)) break;
        }

        return await PrimitiveResult.CombineAll(resultList.ToArray()).ConfigureAwait(false);
    }
    public static async ValueTask<PrimitiveResult> BindAll<TIn>(IEnumerable<TIn> list, Func<TIn, ValueTask<PrimitiveResult>> func, BindAllIterationStrategy iterationStrategy = BindAllIterationStrategy.BreakOnFirstError)
    {
        var listCount = list.Count();
        for (var i = 0; i < listCount; i++)
        {
            var currentFuncResult = await func.Invoke(list.ElementAt(i));

            if (currentFuncResult.IsFailure && iterationStrategy.HasFlag(BindAllIterationStrategy.BreakOnFirstError)) break;
        }
        return PrimitiveResult.Success();
    }
    public static async ValueTask<PrimitiveResult> BindAll<TIn>(IEnumerable<TIn> list, Func<TIn, int, ValueTask<PrimitiveResult>> func, BindAllIterationStrategy iterationStrategy = BindAllIterationStrategy.BreakOnFirstError)
    {
        var listCount = list.Count();
        for (var i = 0; i < listCount; i++)
        {
            var currentFuncResult = await func.Invoke(list.ElementAt(i), i);

            if (currentFuncResult.IsFailure && iterationStrategy.HasFlag(BindAllIterationStrategy.BreakOnFirstError)) break;
        }
        return PrimitiveResult.Success();
    }
    #endregion

    #region " Match "
    /// <summary>
    /// Matches the success status of the result to the corresponding functions.
    /// </summary>
    /// <typeparam name="TValue">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    /// The result of the on-success function if the result is a success result, otherwise the result of the failure result.
    /// </returns>
    public static ValueTask<TOut> Match<TValue, TOut>(
        PrimitiveResult<TValue> src,
        Func<TValue, TOut> onSuccess,
        Func<PrimitiveError[], TOut> onFailure) =>
            ValueTask.FromResult(
                src.IsSuccess ? onSuccess(src.Value) : onFailure(src.Errors));

    /// <summary>
    /// Matches the success status of the result to the corresponding functions.
    /// </summary>
    /// <typeparam name="TValue">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    /// The result of the on-success function if the result is a success result, otherwise the result of the failure result.
    /// </returns>
    public static async ValueTask<TOut> Match<TValue, TOut>(
       ValueTask<PrimitiveResult<TValue>> src,
       Func<TValue, TOut> onSuccess,
       Func<PrimitiveError[], TOut> onFailure)
    {
        var taskResult = await src.ConfigureAwait(false);
        return await Match(taskResult, onSuccess, onFailure);
    }

    public static async ValueTask<TOut> Match<TValue, TOut>(
       Task<PrimitiveResult<TValue>> src,
       Func<TValue, TOut> onSuccess,
       Func<PrimitiveError[], TOut> onFailure)
    {
        var taskResult = await src.ConfigureAwait(false);
        return await Match(taskResult, onSuccess, onFailure);
    }

    public static TOut Match<TOut>(
        PrimitiveResult src,
        Func<TOut> onSuccess,
        Func<PrimitiveError[], TOut> onFailure) => src.IsSuccess ? onSuccess() : onFailure(src.Errors);

    public static async ValueTask<TOut> Match<TOut>(
        ValueTask<PrimitiveResult> src,
        Func<TOut> onSuccess,
        Func<PrimitiveError[], TOut> onFailure)
    {
        var taskResult = await src.ConfigureAwait(false);
        return Match(taskResult, onSuccess, onFailure);
    }


    #endregion

    #region " Do "
    public static ValueTask<PrimitiveResult<TValue>> Do<TValue>(PrimitiveResult<TValue> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => true, act);
    public static ValueTask<PrimitiveResult<TValue>> Do<TValue>(ValueTask<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => true, act);
    public static ValueTask<PrimitiveResult<TValue>> Do<TValue>(Task<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => true, act);

    public static ValueTask<PrimitiveResult<TValue>> OnSuccess<TValue>(PrimitiveResult<TValue> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => _.IsSuccess, act);
    public static ValueTask<PrimitiveResult<TValue>> OnSuccess<TValue>(ValueTask<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => _.IsSuccess, act);
    public static ValueTask<PrimitiveResult<TValue>> OnSuccess<TValue>(Task<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => _.IsSuccess, act);

    public static ValueTask<PrimitiveResult<TValue>> OnFailure<TValue>(PrimitiveResult<TValue> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => _.IsFailure, act);
    public static ValueTask<PrimitiveResult<TValue>> OnFailure<TValue>(ValueTask<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => _.IsFailure, act);

    public static ValueTask<PrimitiveResult<TValue>> OnFailure<TValue>(Task<PrimitiveResult<TValue>> src, Action<PrimitiveResult<TValue>> act) => PrimitiveResult.DoIf(src, _ => _.IsFailure, act);

    /// <summary>
    /// Runs an action if meets some prediction on Result 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="src"></param>
    /// <param name="predicate"></param>
    /// <param name="act"></param>
    /// <returns></returns>
    internal static ValueTask<PrimitiveResult<TValue>> DoIf<TValue>(PrimitiveResult<TValue> src,
      Func<PrimitiveResult<TValue>, bool> predicate,
      Action<PrimitiveResult<TValue>> act)
    {
        if (predicate.Invoke(src)) act.Invoke(src);

        return ValueTask.FromResult(src);
    }

    /// <summary>
    /// Runs an action if meets some prediction on Result 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="src"></param>
    /// <param name="predicate"></param>
    /// <param name="act"></param>
    /// <returns></returns>
    internal async static ValueTask<PrimitiveResult<TValue>> DoIf<TValue>(ValueTask<PrimitiveResult<TValue>> src,
       Func<PrimitiveResult<TValue>, bool> predicate,
       Action<PrimitiveResult<TValue>> act) =>
        await PrimitiveResult.DoIf(
            (await src.ConfigureAwait(false)),
            predicate,
            act).ConfigureAwait(false);

    internal async static ValueTask<PrimitiveResult<TValue>> DoIf<TValue>(Task<PrimitiveResult<TValue>> src,
       Func<PrimitiveResult<TValue>, bool> predicate,
       Action<PrimitiveResult<TValue>> act) =>
        await PrimitiveResult.DoIf(
            (await src.ConfigureAwait(false)),
            predicate,
            act).ConfigureAwait(false);

    #endregion

    #region " BindCore "
    private static async ValueTask<PrimitiveResult> _BindValueTaskCore(ValueTask<PrimitiveResult> src, Func<PrimitiveResult, ValueTask<PrimitiveResult>> func)
    {
        var taskResult = await src.ConfigureAwait(false);
        return await func(taskResult).ConfigureAwait(false);
    }
    private static async ValueTask<PrimitiveResult<TOut>> _BindValueTaskCore<TOut>(ValueTask<PrimitiveResult> src, Func<PrimitiveResult, ValueTask<PrimitiveResult<TOut>>> func)
    {
        var taskResult = await src.ConfigureAwait(false);
        return await func(taskResult).ConfigureAwait(false);
    }
    private static async ValueTask<PrimitiveResult<TOut>> _BindValueTaskCore<TIn, TOut>(ValueTask<PrimitiveResult<TIn>> src, Func<PrimitiveResult<TIn>, ValueTask<PrimitiveResult<TOut>>> func)
    {
        var taskResult = await src.ConfigureAwait(false);
        return await func(taskResult).ConfigureAwait(false);
    }

    #endregion
}
[Flags]
public enum BindAllIterationStrategy
{
    BreakOnFirstError = 1,
    GoToLast = 2
}