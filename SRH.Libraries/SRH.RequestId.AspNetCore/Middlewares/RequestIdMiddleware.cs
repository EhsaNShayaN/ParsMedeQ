using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace SRH.RequestId.AspNetCore.Middlewares;

public sealed class RequestIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRequestIdProvider _requestIdProvider;
    private readonly IOptions<RequestIdOptions> _options;

    public RequestIdMiddleware(
        RequestDelegate next,
        IRequestIdProvider requestIdProvider,
        IOptions<RequestIdOptions> options
        )
    {
        this._next = next;
        this._requestIdProvider = requestIdProvider;
        this._options = options;
    }

    public async Task Invoke(HttpContext context, IRequestIdContextFactrory requestIdContextFactrory)
    {
        var requestId = this._requestIdProvider.GenerateId(context);
        var correlationId = GetCorrelationIdFromContext(context);

        try 
        {
            requestIdContextFactrory.Create(requestId, correlationId);

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.TryAdd(this._options.Value.RequestIdHeaderName, requestId);
                if (!string.IsNullOrWhiteSpace(correlationId))
                {
                    context.Response.Headers.TryAdd(this._options.Value.CorrelationIdHeaderName, correlationId);
                }

                return Task.CompletedTask;
            });

            await _next(context);
        }
        catch { throw; }
        finally {
            requestIdContextFactrory.Dispose();
        }
    }

    private string GetCorrelationIdFromContext(HttpContext context)
    {
        var hasCorrelationId = context.Request.Headers.TryGetValue(this._options.Value.CorrelationIdHeaderName, out var cid) && !StringValues.IsNullOrEmpty(cid);
        return hasCorrelationId ? cid.First()! : string.Empty;
    }
}
