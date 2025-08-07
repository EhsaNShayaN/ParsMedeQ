using SRH.RequestId.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace SRH.RequestId.AspNetCore.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseRequestId(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestIdMiddleware>();
    }
}
