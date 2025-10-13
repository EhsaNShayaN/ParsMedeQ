using System.Security.Claims;

namespace ParsMedeQ.Application.Services.UserAuthenticationServices;
public interface IUserAuthenticationTokenService
{
    PrimitiveResult<string> GenerateToken(string id);
    PrimitiveResult<ClaimsPrincipal> ValidateToken(string token);
}
