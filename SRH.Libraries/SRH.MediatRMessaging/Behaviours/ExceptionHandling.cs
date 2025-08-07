using SRH.MediatRMessaging.Exceptions;
using SRH.PrimitiveTypes.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SRH.MediatRMessaging.Behaviours;

/*
    Behaviour #1
 */
public sealed class ExceptionHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionHandlingBehaviour<TRequest, TResponse>> _logger;

    public ExceptionHandlingBehaviour(ILogger<ExceptionHandlingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next().ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is PrimitiveValidationException)
        {
            var exception = ex as PrimitiveValidationException;
            var stateGenericArgs = typeof(TResponse).GetGenericArguments();
            if (!(stateGenericArgs?.Any() ?? false) || stateGenericArgs.Length != 1)
            {
                throw;
            }
            var result = typeof(PrimitiveResult<>)
                        .MakeGenericType(stateGenericArgs[0])
                        .GetMethod(nameof(PrimitiveResult.Failure))!
                        .Invoke(null, [exception!.Errors.ToArray()]);
            return (TResponse)result!;
        }
        catch (Exception ex)
        {

            this._logger.LogError(ex, "Unhandled exception for {request_name}", typeof(TRequest).Name);
            throw;
        }

    }
}
