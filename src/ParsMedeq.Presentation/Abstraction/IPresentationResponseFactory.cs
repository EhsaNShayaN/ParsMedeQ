using Microsoft.AspNetCore.Http;

namespace EShop.Presentation.Abstraction;

interface IPresentationResponseFactory
{
    IResult Create<TEndpointResponse>(
        PrimitiveResult<TEndpointResponse> apiResponse,
        Func<DefaultApiResponse<TEndpointResponse>, IResult> okFunc);
}
