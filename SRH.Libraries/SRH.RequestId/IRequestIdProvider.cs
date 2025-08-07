using Microsoft.AspNetCore.Http;

namespace SRH.RequestId;

public interface IRequestIdProvider
{
    string GenerateId(HttpContext context);
}