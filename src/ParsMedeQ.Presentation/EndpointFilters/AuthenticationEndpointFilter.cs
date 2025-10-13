using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Services.UserContextAccessorServices;
using System.Net;

namespace ParsMedeQ.Presentation.EndpointFilters;

public sealed class AuthenticationEndpointFilter : IEndpointFilter
{
    private readonly IUserContextAccessor _tspUserContextAccessor;

    public AuthenticationEndpointFilter(IUserContextAccessor tspUserContextAccessor)
    {
        this._tspUserContextAccessor = tspUserContextAccessor;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (this._tspUserContextAccessor.IsGuest())
        {
            return TypedResults.Json(DefaultApiResponse.Failure("", new DefaultApiError("", "کاربر غیر مجاز")), statusCode: HttpStatusCode.Forbidden.GetHashCode());
        }
        return await next(context);
    }
}
