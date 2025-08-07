using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EShop.Presentation.GlobalExceptionHandlers;
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        this._logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        this._logger.LogCritical(exception, "Exception occurred: {Message}", exception.Message);

        var exceptionType = exception.GetType();

        if (exception is BadHttpRequestException)
        {
            httpContext.Response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
            await httpContext.Response
                .WriteAsJsonAsync(
                DefaultApiResponse.Failure(string.Empty, new DefaultApiError("Error", "Bad request")), cancellationToken);
            return true;
        }

        await httpContext.Response
                .WriteAsJsonAsync(
                DefaultApiResponse.Failure(string.Empty, new DefaultApiError("Error", "Server Error")), cancellationToken);

        return true;
    }
}
