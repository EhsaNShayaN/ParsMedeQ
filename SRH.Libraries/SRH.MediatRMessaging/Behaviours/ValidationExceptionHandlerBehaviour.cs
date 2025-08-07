using SRH.MediatRMessaging.Exceptions;
using SRH.PrimitiveTypes.Result;
using MediatR.Pipeline;

namespace SRH.MediatRMessaging.Behaviours;

public sealed class ValidationExceptionHandlerBehaviour<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : notnull
    where TException : PrimitiveValidationException
{
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var stateGenericArgs = state.GetType().GetGenericArguments()[0].GetGenericArguments();

        if ((stateGenericArgs?.Any() ?? false) && stateGenericArgs.Length == 1)
        {
            var result = typeof(PrimitiveResult<>)
                    .MakeGenericType(stateGenericArgs[0])
                    .GetMethod(nameof(PrimitiveResult.Failure))!
                    .Invoke(null, new object[] { exception.Errors.ToArray() });
            state.SetHandled((TResponse)result!);
        }

        return Task.CompletedTask;
    }
}