using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using ParsMedeQ.Application.Services.UserLangServices;
using ParsMedeQ.Domain;

namespace ParsMedeQ.Presentation.Services.ApplicationServices.UserLangServices;
public sealed class UserLangContextAccessor : IUserLangContextAccessor
{
    private static readonly AsyncLocal<UserLangContext?> _current = new();
    public UserLangContext? Current
    {
        get => _current.Value ?? UserLangContext.Empty;
        set => _current.Value = value;
    }
    public UserLangContext GetCurrent() => Current ?? UserLangContext.Empty;
    public string GetCurrentLang() => GetCurrent().Lang;
}
public sealed class UserLangContextAccessorMiddleware
{
    private readonly RequestDelegate _next;

    public UserLangContextAccessorMiddleware(RequestDelegate requestDelegate)
    {
        this._next = requestDelegate;
    }

    public async Task Invoke(HttpContext httpContext,
        IUserLangContextAccessor userLangContextAccessor,
        ILogger<UserLangContextAccessorMiddleware> logger)
    {
        try
        {
            StringValues lang = Constants.LangCode_Farsi.ToLower();
            if (httpContext.Request.Headers.TryGetValue("accept-language", out var r) && !string.IsNullOrWhiteSpace(r))
            {
                lang = r;
            }
            else
            {
                httpContext.Request.Query.TryGetValue("lang", out lang);
            }
            userLangContextAccessor.Current = new UserLangContext(lang.ToString());
            await this._next(httpContext);
        }
        catch
        {
            userLangContextAccessor.Current = new UserLangContext(Constants.LangCode_Farsi.ToLower());
            await this._next(httpContext);
        }
        finally
        {
            userLangContextAccessor.Current = null;
        }
    }
}