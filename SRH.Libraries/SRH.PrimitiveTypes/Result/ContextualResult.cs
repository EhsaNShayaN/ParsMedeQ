namespace SRH.PrimitiveTypes.Result;

public readonly partial record struct ContextualResult<TContext>
{
    private readonly PrimitiveResult<TContext> _context;

    public PrimitiveResult<TContext> Context => _context;

    public bool IsSuccess => _context.IsSuccess;
    public bool IsFailure => _context.IsFailure;

    internal ContextualResult(PrimitiveResult<TContext> context)
    {
        _context = context;
    }

    public static ContextualResult<TContext> Create(TContext context) => new(PrimitiveResult.Success(context));
    public static ContextualResult<TContext> Failure(PrimitiveError error) => new(PrimitiveResult.Failure<TContext>(error));
    public static ContextualResult<TContext> Failure(PrimitiveError[] errors) => new(PrimitiveResult.Failure<TContext>(errors));

    public static ValueTask<ContextualResult<TContext>> Do(ContextualResult<TContext> src,
        Action<PrimitiveResult<TContext>>? success,
        Action<PrimitiveResult<TContext>>? error)
    {
        if (src.IsSuccess)
        {
            success?.Invoke(src.Context);
        }
        else
        {
            error?.Invoke(src.Context);
        }

        return ValueTask.FromResult(src);
    }
    public async static ValueTask<ContextualResult<TContext>> Do(ValueTask<ContextualResult<TContext>> src,
        Action<PrimitiveResult<TContext>>? success,
        Action<PrimitiveResult<TContext>>? error)
    {
        var srcResult = await src.ConfigureAwait(false);
        return await Do(srcResult, success, error).ConfigureAwait(false);
    }
    public static ValueTask<ContextualResult<TContext>> OnSuccess(ContextualResult<TContext> src, Action<PrimitiveResult<TContext>> success) => Do(src, success, null);
    public static ValueTask<ContextualResult<TContext>> OnFailure(ContextualResult<TContext> src, Action<PrimitiveResult<TContext>> error) => Do(src, null, error);
    public static ValueTask<ContextualResult<TContext>> OnSuccess(ValueTask<ContextualResult<TContext>> src, Action<PrimitiveResult<TContext>> success) => Do(src, success, null);
    public static ValueTask<ContextualResult<TContext>> OnFailure(ValueTask<ContextualResult<TContext>> src, Action<PrimitiveResult<TContext>> error) => Do(src, null, error);


    public static ValueTask<ContextualResult<TContext>> Execute(ContextualResult<TContext> src, Func<TContext, TContext> func) =>
        src.Context
            .Bind(ctx => func(ctx))
            .Match(
                ContextualResult<TContext>.Create,
                ContextualResult<TContext>.Failure);

    public static ValueTask<ContextualResult<TContext>> Execute(ContextualResult<TContext> src, Func<TContext, PrimitiveResult<TContext>> func) =>
        src.Context
            .Bind(ctx => func(ctx))
            .Match(
                ContextualResult<TContext>.Create,
                ContextualResult<TContext>.Failure);

    public static ValueTask<ContextualResult<TContext>> Execute(ContextualResult<TContext> src, Func<TContext, ValueTask<PrimitiveResult<TContext>>> func) =>
        src.Context
            .Bind(ctx => func(ctx))
            .Match(
                success => ContextualResult<TContext>.Create(success),
                errors => ContextualResult<TContext>.Failure(errors));

    public static ValueTask<ContextualResult<TContext>> Execute(ContextualResult<TContext> src, Func<TContext, Task<PrimitiveResult<TContext>>> func) =>
       src.Context
           .Bind(ctx => func(ctx))
           .Match(
               ContextualResult<TContext>.Create,
               ContextualResult<TContext>.Failure);


    public async static ValueTask<ContextualResult<TContext>> Execute(ValueTask<ContextualResult<TContext>> src, Func<TContext, TContext> func) => await Execute(await src.ConfigureAwait(false), func).ConfigureAwait(false);

    public async static ValueTask<ContextualResult<TContext>> Execute(ValueTask<ContextualResult<TContext>> src, Func<TContext, PrimitiveResult<TContext>> func) => await Execute(await src.ConfigureAwait(false), func).ConfigureAwait(false);

    public async static ValueTask<ContextualResult<TContext>> Execute(ValueTask<ContextualResult<TContext>> src, Func<TContext, ValueTask<PrimitiveResult<TContext>>> func) => await Execute(await src.ConfigureAwait(false), func).ConfigureAwait(false);

    public async static ValueTask<ContextualResult<TContext>> Execute(ValueTask<ContextualResult<TContext>> src, Func<TContext, Task<PrimitiveResult<TContext>>> func) => await Execute(await src.ConfigureAwait(false), func).ConfigureAwait(false);

    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(ContextualResult<TContext> src, Func<TContext, PrimitiveResult<TOut>> mapper) => src.Context.Map(ctx => mapper(ctx));
    public static ValueTask<PrimitiveResult<TOut>> Map<TOut>(ContextualResult<TContext> src, Func<TContext, TOut> mapper) => src.Context.Map(ctx => mapper(ctx));
    public async static ValueTask<PrimitiveResult<TOut>> Map<TOut>(ValueTask<ContextualResult<TContext>> src, Func<TContext, PrimitiveResult<TOut>> mapper) => await Map(await src.ConfigureAwait(false), mapper).ConfigureAwait(false);
    public async static ValueTask<PrimitiveResult<TOut>> Map<TOut>(ValueTask<ContextualResult<TContext>> src, Func<TContext, TOut> mapper) => await Map(await src.ConfigureAwait(false), mapper).ConfigureAwait(false);
}
