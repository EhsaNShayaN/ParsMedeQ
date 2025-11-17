using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using System.Net;

namespace ParsMedeQ.Presentation.EndpointFilters;

public sealed class AuthorizationEndpointFilter : IEndpointFilter
{
    private readonly IUserContextAccessor _userContextAccessor;

    public AuthorizationEndpointFilter(IUserContextAccessor userContextAccessor)
    {
        this._userContextAccessor = userContextAccessor;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var UserId = this._userContextAccessor.GetCurrent().UserId;
        if (UserId is not 1 and not 2)
        {
            return TypedResults.Json(DefaultApiResponse.Failure("", new DefaultApiError("", "کاربر غیر مجاز")), statusCode: HttpStatusCode.Forbidden.GetHashCode());
        }
        return await next(context);
    }
}
