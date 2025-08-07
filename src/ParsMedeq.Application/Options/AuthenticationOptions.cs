using Microsoft.AspNetCore.Http;

namespace ParsMedeq.Application.Options;

public sealed class AuthenticationOptions
{
    public string IssuerName { get; set; } = "PARS_MEDEQ";
    public string TokenCookieName { get; set; } = "user-token";
    public string RefreshTokenCookieName { get; set; } = "user-refresh-token";
    public string CSRFTokenCookieName { get; set; } = "CSRF-TOKEN";
    public string CSRFTokenHeaderName { get; set; } = "X-CSRF-TOKEN";
    public bool EnableAntiForgeryOnAllEndpoints { get; set; } = true;
    public CookieSecurePolicy AntiForgeyCookieSecurePolicy { get; set; } = CookieSecurePolicy.Always;
    public TimeSpan TokenExpDuration { get; set; } = TimeSpan.FromSeconds(30);
    public TimeSpan RefreshTokenExpDuration { get; set; } = TimeSpan.FromHours(2);
    public CookieOptions CookieOptions { get; set; } = new CookieOptions()
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Secure = true
    };
}