using SRH.MediatRMessaging.Exceptions;
using SRH.PrimitiveTypes.Result;
using FluentValidation;
using MediatR;

namespace SRH.MediatRMessaging.FluentValidation.Behaviours;

public sealed class FluentValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public FluentValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next().ConfigureAwait(false);
        }

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(failrures => failrures is not null)
            .Select(failrure => PrimitiveError.Create(failrure.PropertyName, failrure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            ThrowHandlerException(errors);
        }

        return await next().ConfigureAwait(false);
    }

    private static TResponse ThrowHandlerException(PrimitiveError[] errors) => throw new PrimitiveValidationException(errors);
}
