using SRH.PrimitiveTypes.Result;

namespace SRH.MediatRMessaging.Exceptions;

public class PrimitiveValidationException : Exception
{
    PrimitiveError[] _errors;

    public PrimitiveValidationException(PrimitiveError[] errors)
    {
        _errors = errors;
    }

    public IReadOnlyCollection<PrimitiveError> Errors => _errors.AsReadOnly();
}
