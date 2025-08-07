namespace SRH.PrimitiveTypes.Result;
public class FaultTolerantPipeline<TContext>
{
    protected List<Func<TContext, ValueTask<PrimitiveResult<TContext>>>> _pipeline = new();
    protected Func<Exception, ValueTask<PrimitiveResult<TContext>>>? _exceptionHandler = null;
    protected Func<PrimitiveResult<TContext>, Task>? _defer = null;

    private readonly PrimitiveResult<TContext> _context;

    public PrimitiveResult<TContext> Context => _context;

    public bool IsSuccess => _context.IsSuccess;
    public bool IsFailure => _context.IsFailure;

    protected FaultTolerantPipeline(PrimitiveResult<TContext> context)
    {
        _context = context;
    }

    public static FaultTolerantPipeline<TContext> Create(TContext context) => new(PrimitiveResult.Success(context));


    public FaultTolerantPipeline<TContext> Execute(Func<TContext, ValueTask<PrimitiveResult<TContext>>> func)
    {
        this._pipeline.Add(func);
        return this;
    }
    public FaultTolerantPipeline<TContext> Execute(Func<TContext, TContext> func)
    {
        this._pipeline.Add(ctx => ValueTask.FromResult(PrimitiveResult.Success(func.Invoke(ctx))));
        return this;
    }
    public FaultTolerantPipeline<TContext> Execute(Func<TContext, PrimitiveResult<TContext>> func)
    {
        this._pipeline.Add(ctx => ValueTask.FromResult(func.Invoke(ctx)));
        return this;
    }
    public FaultTolerantPipeline<TContext> HandleException(Func<Exception, ValueTask<PrimitiveResult<TContext>>> func)
    {
        this._exceptionHandler = func;
        return this;
    }
    public FaultTolerantPipeline<TContext> Defer(Func<PrimitiveResult<TContext>, Task> deferAction)
    {
        this._defer = deferAction;
        return this;
    }

    public async ValueTask<PrimitiveResult<TContext>> Run()
    {
        if (this._context.IsFailure) return this._context;

        var result = this.Context;
        var ctx = this.Context.Value;

        try
        {
            var l = this._pipeline.Count;
            for (int i = 0; i < l; i++)
            {
                var func = this._pipeline[i];
                result = await func.Invoke(ctx).ConfigureAwait(false);

                if (result.IsFailure) return result;
                ctx = result.Value;
            }
            return result;
        }
        catch (Exception ex)
        {
            if (this._exceptionHandler is not null)
            {
                return await this._exceptionHandler(ex).ConfigureAwait(false);
            }
            return PrimitiveResult.Failure<TContext>("Internal.Exception", ex.Message);
        }
        finally
        {
            if (this._defer is not null)
            {
                await this._defer.Invoke(result).ConfigureAwait(false);
            }
        }
    }
}
