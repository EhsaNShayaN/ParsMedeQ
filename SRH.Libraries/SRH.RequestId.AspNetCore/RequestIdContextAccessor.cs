namespace SRH.RequestId.AspNetCore;

public sealed class RequestIdContextAccessor : IRequestIdContextAccessor
{
    private static AsyncLocal<RequestIdContext?> _current = new AsyncLocal<RequestIdContext?>();
    public RequestIdContext? Current
    {
        set => _current!.Value = value;
    }

    public RequestIdContext GetCurrentRequestIdContext() => _current?.Value ?? new RequestIdContext(Guid.NewGuid().ToString(), string.Empty);
}
