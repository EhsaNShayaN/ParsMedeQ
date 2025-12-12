using Microsoft.AspNetCore.Http;

namespace ParsMedeQ.Presentation.Services.ApplicationServices.UserContextAccessorServices;

public sealed class IpRestrictionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _allowedIp;

    public IpRestrictionMiddleware(RequestDelegate next, string allowedIp)
    {
        _next = next;
        _allowedIp = allowedIp;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress;

        if (remoteIp == null || !remoteIp.ToString().Equals(_allowedIp))
        {
            context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
            await context.Response.WriteAsync("Forbidden: Your IP is not allowed.\nContact your website designer.");
            return;
        }

        await _next(context);
    }
}

// Extension method
public static class IpRestrictionMiddlewareExtensions
{
    public static IApplicationBuilder UseIpRestriction(this IApplicationBuilder builder, string allowedIp)
    {
        return builder.UseMiddleware<IpRestrictionMiddleware>(allowedIp);
    }
}
