using SRH.MediatRMessaging.Exceptions;
using SRH.PrimitiveTypes.Maybe;
using SRH.PrimitiveTypes.Result;
using MediatR;

namespace SRH.MediatRMessaging.Behaviours;

/*
    Behaviour #3
 */
public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IValidatableRequest<TRequest>, IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) =>
        await PrimitiveMaybe
            .From(request)
            .DoOrFailure(
                request => PrimitiveResult
                            .Success(request)
                            .Bind(_ => _.Validate())
                            .Map(_ => next())
                            .Match(
                                async success => await success.ConfigureAwait(false),
                                errors => ThrowHandlerException(errors)
                            ),
                new Exception("request is null"))
            .ConfigureAwait(false);
    private static TResponse ThrowHandlerException(PrimitiveError[] errors) => throw new PrimitiveValidationException(errors);
}
