using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ResourceCategoryFeatures.ResourceCategoryDetailsFeature;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceCategoryListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceCategoryContracts.ResourceCategoryDetailsContract;
using ParsMedeQ.Contracts.ResourceCategoryContracts.ResourceCategoryListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ResourceCategoryFeatures.ResourceCategoryDetails;

sealed class ResourceCategoryDetailsEndpoint : EndpointHandlerBase<
    ResourceCategoryDetailsApiRequest,
    ResourceCategoryDetailsQuery,
    ResourceCategoryListDbQueryResponse,
    ResourceCategoryDetailsApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public ResourceCategoryDetailsEndpoint(
        IPresentationMapper<ResourceCategoryDetailsApiRequest, ResourceCategoryDetailsQuery> requestMapper,
        IPresentationMapper<ResourceCategoryListDbQueryResponse, ResourceCategoryDetailsApiResponse> responseMapper)
        : base(
            Endpoints.Resource.ResourceCategory,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ResourceCategoryDetailsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new ResourceCategoryDetailsQuery(request.Id))),
            cancellationToken);

}
sealed class ResourceCategoryDetailsApiRequestMapper : IPresentationMapper<
    ResourceCategoryDetailsApiRequest,
    ResourceCategoryDetailsQuery>
{
    public ValueTask<PrimitiveResult<ResourceCategoryDetailsQuery>> Map(
        ResourceCategoryDetailsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new ResourceCategoryDetailsQuery(src.Id)));
    }
}
sealed class ResourceCategoryDetailsApiResponseMapper : IPresentationMapper<
    ResourceCategoryListDbQueryResponse,
    ResourceCategoryDetailsApiResponse>
{
    public ValueTask<PrimitiveResult<ResourceCategoryDetailsApiResponse>> Map(
        ResourceCategoryListDbQueryResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new ResourceCategoryDetailsApiResponse(
                        src.Id,
                        src.TableId,
                        src.Title,
                        src.Description,
                        src.Count,
                        src.ParentId,
                        src.CreationDate.ToPersianDate())
                    ));
    }
}