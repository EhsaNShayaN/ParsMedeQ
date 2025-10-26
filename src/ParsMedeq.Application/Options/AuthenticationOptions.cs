using Microsoft.AspNetCore.Http;

namespace ParsMedeQ.Application.Options;

public sealed class AuthenticationOptions
{
    public string IssuerName { get; set; } = "PARSMEDEQ";
    public string AuthorizationHeaderName { get; set; } = "Authorization";
    public string TokenCookieName { get; set; } = "parsmedeq-token";
    public string ClientTokenCookieName { get; set; } = "parsmedeq";
    public string RefreshTokenCookieName { get; set; } = "parsmedeq-refresh-token";
    public string CSRFTokenCookieName { get; set; } = "PARSMEDEQ-CSRF-TOKEN";
    public string CSRFTokenHeaderName { get; set; } = "PARSMEDEQ-X-CSRF-TOKEN";
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
    public string SecretKey { get; set; } = "a4d6f9c1b7e48d2f6c5a9e0b3f7d4a1c8e2f9b7a5d4c3e6f8a9b1c2d3e4f5a6";
}