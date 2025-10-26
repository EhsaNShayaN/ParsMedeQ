using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using System.Net;

namespace ParsMedeQ.Presentation.EndpointFilters;

public sealed class AuthorizationEndpointFilter : IEndpointFilter
{
    private readonly IUserContextAccessor _tspUserContextAccessor;

    public AuthorizationEndpointFilter(IUserContextAccessor tspUserContextAccessor)
    {
        this._tspUserContextAccessor = tspUserContextAccessor;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var UserId = this._tspUserContextAccessor.GetCurrent().UserId;
        if (UserId is not 1 and not 2)
        {
            return TypedResults.Json(DefaultApiResponse.Failure("", new DefaultApiError("", "کاربر غیر مجاز")), statusCode: HttpStatusCode.Forbidden.GetHashCode());
        }
        return await next(context);
    }
}
