namespace SRH.RequestId;

public sealed class RequestIdOptions
{
    public const string DefaultRequestIdHeaderName = "x-Request-Id";
    public const string DefaultRequestCorrelationIdHeaderName = "x-Correlation-Id";

    private string? _requestIdHeaderName = null;
    private string? _correlationIdHeaderName = null;

    public string RequestIdHeaderName
    {
        set => this._requestIdHeaderName = value ?? DefaultRequestIdHeaderName;
        get => this._requestIdHeaderName ?? DefaultRequestIdHeaderName;
    }
    public string CorrelationIdHeaderName
    {
        set => this._correlationIdHeaderName = value ?? DefaultRequestCorrelationIdHeaderName;
        get => this._correlationIdHeaderName ?? DefaultRequestCorrelationIdHeaderName;
    }
}

