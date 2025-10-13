using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using System.Diagnostics;

namespace ParsMedeQ.Presentation.EndpointFilters;

public sealed class EndpointCallEndpointFilter : IEndpointFilter
{
    private readonly IUserContextAccessor _tspUserContextAccessor;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<EndpointCallEndpointFilter> _logger;

    public EndpointCallEndpointFilter(
        IUserContextAccessor tspUserContextAccessor,
        IHttpContextAccessor httpContextAccessor,
        ILogger<EndpointCallEndpointFilter> logger)
    {
        this._tspUserContextAccessor = tspUserContextAccessor;
        this._httpContextAccessor = httpContextAccessor;
        this._logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        var message = "user with id '{userid}' called endpoint : '{endpoint}' at '{request_time}'. duration : {duration}ms";

        var result = await next(context);

        stopwatch.Stop();

        var currentRequestor = this._tspUserContextAccessor.GetCurrent();

        this._logger.LogInformation(message,
            currentRequestor.UserId,
            this._httpContextAccessor.HttpContext!.Request.Path.ToString().ToLower().Trim(),
            DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            stopwatch.ElapsedMilliseconds);

        return result;
    }
}