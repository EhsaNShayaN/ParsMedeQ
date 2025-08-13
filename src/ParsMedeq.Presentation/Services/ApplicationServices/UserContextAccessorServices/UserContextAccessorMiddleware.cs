using ParsMedeQ.Application.Persistance.ESopSchema;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Helpers;
using ParsMedeQ.Domain.Types.UserId;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ParsMedeQ.Presentation.Services.ApplicationServices.UserContextAccessorServices;

public sealed class UserContextAccessorMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextAccessorMiddleware(RequestDelegate requestDelegate)
    {
        this._next = requestDelegate;
    }

    public async Task Invoke(HttpContext httpContext,
        IUserContextAccessor UserContextAccessor,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<UserContextAccessorMiddleware> logger, CancellationToken cancellationToken)
    {
        UserContext taxPayerUserContext = UserContext.Guest;
        try
        {
            if (httpContext.Request.Query.TryGetValue("userId", out var userId))
            {
                using var scope = serviceScopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IReadUnitOfWork>().UserReadRepository;

                if (HashIdsHelper.Instance.TryDecodeSingle(userId, out int uid))
                {
                    var userInfoResult = await repo.FindUser(
                        UserIdType.FromDb(uid),
                        cancellationToken);

                    if (userInfoResult.IsSuccess)
                    {
                        var userInfo = userInfoResult.Value;
                        UserContextAccessor.Current = new UserContext(
                            userInfo.Id,
                            new UserInfo(
                                userInfo.Email,
                                userInfo.Mobile,
                                userInfo.IsMobileConfirmed,
                                userInfo.IsEmailConfirmed,
                                userInfo.FullName));
                    }
                }
            }
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