using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParsMedeQ.Application.Services.TokenGeneratorService;
using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Presentation.Services.ApplicationServices.UserContextAccessorServices;

public sealed class UserContextAccessorMiddleware
{
    const string _authorizationHeaderName = "Authorization";
    private readonly RequestDelegate _next;

    public UserContextAccessorMiddleware(RequestDelegate requestDelegate)
    {
        this._next = requestDelegate;
    }

    public async Task Invoke(HttpContext httpContext,
        IUserContextAccessor UserContextAccessor,
        IServiceScopeFactory serviceScopeFactory,
        ITokenGeneratorService userAuthenticationTokenService,
        ILogger<UserContextAccessorMiddleware> logger)
    {
        UserContext currentUser = UserContext.Guest;
        try
        {
            if (httpContext.Request.Headers.TryGetValue(_authorizationHeaderName, out var authHeader)
                && !string.IsNullOrWhiteSpace(authHeader))
            {
                var tokenItems = authHeader.ToString().Split(" ");
                if (tokenItems.Length == 2 && tokenItems.First().Equals("bearer", StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = await userAuthenticationTokenService.ValidateToken(tokenItems.Last(), true, string.Empty, httpContext.RequestAborted)
                        .Map(AuthenticationHelper.GetUserId)
                        .Match(
                            s => s,
                            _ => 0)
                        .ConfigureAwait(false);
                    if (userId > 0)
                    {
                        currentUser = new UserContext(userId);
                    }
                }
            }

            UserContextAccessor.Current = currentUser;
            await this._next(httpContext);
        }
        catch
        {
            throw;
        }
        finally
        {
            UserContextAccessor.Current = null;
        }
    }
}