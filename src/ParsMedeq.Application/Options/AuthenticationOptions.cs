using Microsoft.AspNetCore.Http;

namespace ParsMedeQ.Application.Options;

public sealed class AuthenticationOptions
{
    public string IssuerName { get; set; } = "PARS_MEDEQ";
    public string AuthorizationHeaderName { get; set; } = "Authorization";
    public string TokenCookieName { get; set; } = "user-token";
    public string ClientTokenCookieName { get; set; } = "token";
    public string RefreshTokenCookieName { get; set; } = "user-refresh-token";
    public string CSRFTokenCookieName { get; set; } = "CSRF-TOKEN";
    public string CSRFTokenHeaderName { get; set; } = "X-CSRF-TOKEN";
    public bool EnableAntiForgeryOnAllEndpoints { get; set; } = true;
    public CookieSecurePolicy AntiForgeyCookieSecurePolicy { get; set; } = CookieSecurePolicy.Always;
    public TimeSpan TokenExpDuration { get; set; } = TimeSpan.FromDays(1);
    public TimeSpan RefreshTokenExpDuration { get; set; } = TimeSpan.FromHours(2);
    public CookieOptions CookieOptions { get; set; } = new CookieOptions()
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Secure = true
    };
    public string SecretKey { get; set; } = "a3d6f9c1b7e48d2f6c5a9e0b3f7d4a1c8e2f9b7a5d4c3e6f8a9b1c2d3e4f5a6";
}