using Microsoft.AspNetCore.Http;
using ParsMedeQ.Domain.Helpers;
using System.Security.Claims;

namespace ParsMedeQ.Application.Services.TokenGeneratorService;
public interface ITokenGeneratorService
{
    ValueTask<PrimitiveResult<string>> GenerateToken(long userId, string audience, CancellationToken cancellationToken);
    void PersistToken(string value, HttpContext httpContextAccessor);
    void LogOut(HttpContext httpContextAccessor);
    ValueTask<PrimitiveResult<ClaimsPrincipal>> ValidateToken(string token, bool validateLifetime, string validAudience, CancellationToken cancellationToken);
}
public static class AuthenticationHelper
{
    public const string USERID_CLAIM = "userid";
    public const string WEB_AUDIENCE = "web";
    public const string MOBILE_AUDIENCE = "mobile";


    public static int GetUserId(ClaimsPrincipal? claims)
    {
        var result = GetClaim(claims, AuthenticationHelper.USERID_CLAIM);

        if (string.IsNullOrWhiteSpace(result)) return 0;

        if (HashIdsHelper.Instance.TryDecodeSingle(result, out var id) && id > 0)
        {
            return id;
        }

        return 0;
    }

    public static string GenerateTokenUserId(long userId)
    {
        return HashIdsHelper.Instance.EncodeLong(userId);
    }
    public static string GetAudience(ClaimsPrincipal? claims) => GetClaim(claims, "aud");
    static string GetClaim(ClaimsPrincipal? claims, string claimType)
    {
        if (claims is null) return string.Empty;

        if (!claims.Claims.Any()) return string.Empty;

        var result = claims.FindFirstValue(claimType);

        return result ?? string.Empty;
    }
}
