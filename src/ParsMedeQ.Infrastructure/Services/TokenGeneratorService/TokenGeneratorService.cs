using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParsMedeQ.Application.Options;
using ParsMedeQ.Application.Services.TokenGeneratorService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ParsMedeQ.Infrastructure.Services.UserAuthenticationToken;
public sealed class TokenGeneratorService : ITokenGeneratorService
{
    private readonly AuthenticationOptions _authenticationOptions;

    public TokenGeneratorService(
        IOptionsMonitor<AuthenticationOptions> authenticationOptions)
    {
        this._authenticationOptions = authenticationOptions.CurrentValue;
    }
    public ValueTask<PrimitiveResult<string>> GenerateToken(
        long userId,
        string audience,
        CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(AuthenticationHelper.USERID_CLAIM,  AuthenticationHelper.GenerateTokenUserId(userId))
        };
        return this.GetTokenSigningCredentials(cancellationToken)
        .Map(signingCredentials =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = this._authenticationOptions.IssuerName,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(this._authenticationOptions.TokenExpDuration),
                SigningCredentials = signingCredentials,
                Audience = audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        });
    }
    public void PersistToken(string value, HttpContext httpContext)
    {
        httpContext.Response.Cookies.Append(
            this._authenticationOptions.TokenCookieName,
            value,
            new CookieOptions()
            {
                HttpOnly = false,
                SameSite = this._authenticationOptions.CookieOptions.SameSite,
                Secure = this._authenticationOptions.CookieOptions.Secure
            });

        httpContext.Response.Cookies.Append(
            this._authenticationOptions.TokenCookieName,
            value,
            this._authenticationOptions.CookieOptions);
    }
    public ValueTask<PrimitiveResult<ClaimsPrincipal>> ValidateToken(
        string token,
        bool validateLifetime,
        string validAudience,
        CancellationToken cancellationToken)
    {
        return this.GetSecurityKey(cancellationToken)
            .Map(securityKey => new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = !string.IsNullOrWhiteSpace(validAudience),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = validateLifetime,
                ValidIssuers = [this._authenticationOptions.IssuerName],
                ValidAudience = validAudience,
            })
            .Map(validationParameters =>
            {
                ClaimsPrincipal? tokenClaimsPrincipal = null;
                try
                {
                    tokenClaimsPrincipal = new JwtSecurityTokenHandler()
                        .ValidateToken(token, validationParameters, out var validatedToken);

                    var jwtToken = validatedToken as JwtSecurityToken;
                    if (jwtToken == null || tokenClaimsPrincipal is null || (tokenClaimsPrincipal.Claims?.Count() ?? 0) <= 0)
                    {
                        return PrimitiveResult.InternalFailure<ClaimsPrincipal>("Token.Validation.Error", "Invalid token");
                    }

                    return tokenClaimsPrincipal;

                }
                catch (SecurityTokenExpiredException e1)
                {
                    return PrimitiveResult.Failure<ClaimsPrincipal>("", "Security Token Expired.");
                }
                catch (Exception ex)
                {
                    return PrimitiveResult.Failure<ClaimsPrincipal>("", ex.Message);
                }
            }
            );
    }
    public void LogOut(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Delete(this._authenticationOptions.TokenCookieName);
        httpContext.Response.Cookies.Delete(this._authenticationOptions.RefreshTokenCookieName);
    }

    ValueTask<PrimitiveResult<SigningCredentials>> GetTokenSigningCredentials(CancellationToken cancellationToken)
    {
        return this.GetSecurityKey(cancellationToken)
            .Map(key => new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
    }
    ValueTask<PrimitiveResult<SymmetricSecurityKey>> GetSecurityKey(CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._authenticationOptions.SecretKey));
        return ValueTask.FromResult(PrimitiveResult.Success(securityKey));
    }
}

public sealed class UserAuthenticationTokenServiceOptions
{
    public string SecretKey { get; set; } = "a3d6f9c1b7e48d2f6c5a9e0b3f7d4a1c8e2f9b7a5d4c3e6f8a9b1c2d3e4f5a6";
    public string Issuer { get; set; } = "Bazneshastegi";
    public string[] Audiences { get; set; } = [];
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateAudience { get; set; } = true;
    public TimeSpan Expiry { get; set; } = TimeSpan.FromDays(1);
}