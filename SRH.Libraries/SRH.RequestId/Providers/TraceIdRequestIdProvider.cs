using Microsoft.AspNetCore.Http;

namespace SRH.RequestId.Providers;

public sealed class TraceIdRequestIdProvider : IRequestIdProvider
{
    public string GenerateId(HttpContext context) => context.TraceIdentifier;
}