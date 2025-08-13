namespace ParsMedeQ.Application.Errors;
public static class ApplicationErrors
{
    public readonly static PrimitiveError TooManyRequestsError = PrimitiveError.Create("Application.Error", "Too many requests ....");

    public static PrimitiveResult<T> CreateTooManyRequestsError<T>() => PrimitiveResult.Failure<T>(TooManyRequestsError);
    public static PrimitiveResult CreateTooManyRequestsError() => PrimitiveResult.Failure(TooManyRequestsError);
}
