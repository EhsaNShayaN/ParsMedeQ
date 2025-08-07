namespace SRH.RequestId.AspNetCore;

public sealed class RequestIdContextFactrory : IRequestIdContextFactrory
{
    private readonly IRequestIdContextAccessor _requestIdContextAccessor;

    public RequestIdContextFactrory(IRequestIdContextAccessor requestIdContextAccessor)
    {
        _requestIdContextAccessor = requestIdContextAccessor;
    }
    public RequestIdContext Create(string requestId, string correlationId)
    {
        var result = new RequestIdContext(requestId, correlationId ?? string.Empty);
        if (this._requestIdContextAccessor is not null)
        {
            this._requestIdContextAccessor.Current = result;
        }
        return result;
    }

    public void Dispose()
    {
        if (this._requestIdContextAccessor is not null)
        {
            this._requestIdContextAccessor.Current = null;
        }
    }
}