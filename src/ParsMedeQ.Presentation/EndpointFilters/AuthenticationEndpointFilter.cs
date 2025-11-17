using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using System.Net;

namespace ParsMedeQ.Presentation.EndpointFilters;

public sealed class AuthenticationEndpointFilter : IEndpointFilter
{
    private readonly IUserContextAccessor _userContextAccessor;

    public AuthenticationEndpointFilter(IUserContextAccessor userContextAccessor)
    {
        this._userContextAccessor = userContextAccessor;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (this._userContextAccessor.IsGuest())
        {
            return TypedResults.Json(DefaultApiResponse.Failure("", new DefaultApiError("", "کاربر غیر مجاز")), statusCode: HttpStatusCode.Forbidden.GetHashCode());
        }
        return await next(context);
    }
}
