using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParsMedeQ.Application.Services.UserAuthenticationServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ParsMedeQ.Infrastructure.Services.UserAuthenticationToken;
public sealed class UserAuthenticationTokenService : IUserAuthenticationTokenService
{
    private readonly IOptionsMonitor<UserAuthenticationTokenServiceOptions> _opts;
    private readonly ILogger<UserAuthenticationTokenService> _logger;

    public UserAuthenticationTokenService(
        IOptionsMonitor<UserAuthenticationTokenServiceOptions> opts,
        ILogger<UserAuthenticationTokenService> logger)
    {
        this._opts = opts;
        this._logger = logger;
    }
    public PrimitiveResult<string> GenerateToken(string id)
    {
        List<Claim> claims = new()
        {
            new Claim("id", id)
        };

        // Add multiple audience claims manually
        foreach (var aud in this._opts.CurrentValue.Audiences)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, aud));
        }


        // Generate a symmetric security key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._opts.CurrentValue.SecretKey));

        // Generate signing credentials
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the token
        var token = new JwtSecurityToken(
            issuer: this._opts.CurrentValue.Issuer,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.Add(this._opts.CurrentValue.Expiry),
            signingCredentials: creds
        );

        // Return the serialized token string
        var result = new JwtSecurityTokenHandler().WriteToken(token);

        return result ?? string.Empty;

    }
    public PrimitiveResult<ClaimsPrincipal> ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(this._opts.CurrentValue.SecretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = this._opts.CurrentValue.ValidateIssuer,
                ValidIssuer = this._opts.CurrentValue.Issuer,

                ValidateAudience = this._opts.CurrentValue.ValidateAudience && this._opts.CurrentValue.Audiences.Length > 0,
                ValidAudiences = this._opts.CurrentValue.Audiences,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                ValidateLifetime = true, // check expiration
                ClockSkew = TimeSpan.Zero // no extra tolerance
            }, out SecurityToken validatedToken);


            return principal ?? PrimitiveResult.InternalFailure<ClaimsPrincipal>("Token.Validation.Error", "Invalid token");
        }
        catch (Exception ex)
        {
            return PrimitiveResult.InternalFailure<ClaimsPrincipal>("Token.Validation.Exception", ex.Message);
        }

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