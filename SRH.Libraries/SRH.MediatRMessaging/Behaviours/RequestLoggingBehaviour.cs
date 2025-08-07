using SRH.PrimitiveTypes.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SRH.MediatRMessaging.Behaviours;

/*
    Behaviour #2
 */
public sealed class RequestLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<RequestLoggingBehaviour<TRequest, TResponse>> _logger;

    public RequestLoggingBehaviour(ILogger<RequestLoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        this._logger.LogInformation("Processing request {request_name}", requestName);
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var response = await next().ConfigureAwait(false);
        sw.Stop();

        if (response is not null)
        {
            var responseType = response.GetType()!;
            var responseGenericType = responseType.GetGenericArguments();

            if (responseGenericType.Length.Equals(1))
            {
                var primitiveResultType = typeof(PrimitiveResult<>)
                    .MakeGenericType(responseGenericType);

                if (primitiveResultType.IsAssignableFrom(response.GetType()))
                {
                    PrimitiveResult result = (PrimitiveResult)primitiveResultType
                        .GetMethod("From")!
                        .Invoke(null, new object[] { response })!;

                    if (result.IsSuccess)
                    {
                        this._logger.LogInformation("Completed request '{request_name}' in '{duration}ms'", requestName, sw.ElapsedMilliseconds);
                    }
                    else
                    {
                        this._logger.LogError("Completed request '{request_name}' with error in '{duration}ms'", requestName, sw.ElapsedMilliseconds);
                    }

                }
            }
        }

        return response;
    }
}
