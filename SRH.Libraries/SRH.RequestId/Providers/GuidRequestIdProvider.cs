using Microsoft.AspNetCore.Http;

namespace SRH.RequestId.Providers;

public sealed class GuidRequestIdProvider : IRequestIdProvider
{
    public string GenerateId(HttpContext context) => Guid.NewGuid().ToString();
}
