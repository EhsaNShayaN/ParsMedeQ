using Microsoft.AspNetCore.Http;

namespace ParsMedeQ.Presentation.Abstraction;

interface IPresentationResponseFactory
{
    IResult Create<TEndpointResponse>(
        PrimitiveResult<TEndpointResponse> apiResponse,
        Func<DefaultApiResponse<TEndpointResponse>, IResult> okFunc);
}
