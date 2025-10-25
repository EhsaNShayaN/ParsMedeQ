using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParsMedeQ.Application.Options;
using ParsMedeQ.Application.Services.TokenGeneratorService;
using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Presentation.Services.ApplicationServices.UserContextAccessorServices;

public sealed class UserContextAccessorMiddleware
{
    private readonly AuthenticationOptions _authenticationOptions;
    private readonly RequestDelegate _next;

    public UserContextAccessorMiddleware(RequestDelegate requestDelegate,
        IOptionsMonitor<AuthenticationOptions> authenticationOptions)
    {
        this._next = requestDelegate;
        this._authenticationOptions = authenticationOptions.CurrentValue;
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
            var requestToken = GetValueFromHeaderOrCoockie(
                this._authenticationOptions.AuthorizationHeaderName,
                this._authenticationOptions.TokenCookieName,
                httpContext);

            if (requestToken is not null)
            {
                var userId = await userAuthenticationTokenService.ValidateToken(requestToken.Value, true, string.Empty, httpContext.RequestAborted)
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
    static ValueResult GetValueFromHeaderOrCoockie(string headerName, string cookieName, HttpContext httpContext)
    {
        if (httpContext.Request.Cookies.TryGetValue(cookieName, out var cookieValue) && !string.IsNullOrWhiteSpace(cookieValue))
            return ValueResult.FromCookie(cookieValue);

        if (httpContext.Request.Headers.TryGetValue(headerName, out var headerValue) && !string.IsNullOrWhiteSpace(headerValue))
            return ValueResult.FromHeader(headerValue.ToString());

        return ValueResult.Empty;
    }
    sealed record ValueResult
    {
        public static ValueResult Empty = new ValueResult() { Value = string.Empty };

        public string Value { get; private set; } = string.Empty;
        public bool Cookie { get; private set; } = false;
        public bool Header { get; private set; } = false;

        public static ValueResult FromHeader(string v) => new ValueResult() { Value = v, Header = true };
        public static ValueResult FromCookie(string v) => new ValueResult() { Value = v, Cookie = true };


    }
}