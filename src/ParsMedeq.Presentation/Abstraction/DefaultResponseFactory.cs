using Microsoft.AspNetCore.Http;

namespace ParsMedeQ.Presentation.Abstraction;

public sealed class DefaultResponseFactory : IPresentationResponseFactory
{
    public readonly static DefaultResponseFactory Instance = new DefaultResponseFactory();
    public IResult Create<TEndpointResponse>(
        PrimitiveResult<TEndpointResponse> apiResponse,
        Func<DefaultApiResponse<TEndpointResponse>, IResult> okFunc)
    {
        if (apiResponse.IsSuccess)
        {
            return okFunc.Invoke(DefaultApiResponse.Success("", apiResponse.Value));
        }
        return TypedResults.Json(DefaultApiResponse.Failure("",
            apiResponse.Errors.Select(e => new DefaultApiError(e.Code, e.Message)).ToArray()
            ));
    }

    public IResult CreateOk<TEndpointResponse>(PrimitiveResult<TEndpointResponse> apiResponse)
        => this.Create(apiResponse, TypedResults.Ok);
}
