namespace ParsMedeQ.Presentation.Abstraction;

public record DefaultApiResponse(
    string RequestId,
    bool IsSuccess,
    DefaultApiError[]? Errors)
{
    public static DefaultApiResponse Success(string requestId) => new DefaultApiResponse(requestId, true, []);
    public static DefaultApiResponse Failure(string requestId, DefaultApiError[] errors) => new DefaultApiResponse(requestId, false, errors);
    public static DefaultApiResponse Failure(string requestId, DefaultApiError error) => new DefaultApiResponse(requestId, false, [error]);
    public static DefaultApiResponse<T> Success<T>(string requestId, T data) => new DefaultApiResponse<T>(requestId, true, data, []);
}
public sealed record DefaultApiResponse<T>(
    string RequestId,
    bool IsSuccess,
    T Data,
    DefaultApiError[]? Errors) : DefaultApiResponse(RequestId, IsSuccess, Errors);
public sealed record DefaultApiError(string ErrorCode, string ErrorMessage);