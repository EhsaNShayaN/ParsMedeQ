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

        var message = "The user '{requestor_id}({requestor_type})' with profile id:'{requestor_profile_id}', branch id:'{requestor_profile_branchId}', branch code:'{requestor_profile_branchCode}', branch name:'{requestor_profile_branchName}', memoryId: '{requestor_profile_memoryId}' called '{endpoint}' at {request_time}. duration is : '{duration}'ms";

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