using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ResourceCategoryFeatures.ResourceCategoryListFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceCategoryContracts.ResourceCategoryListContract;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ResourceCategoryFeatures.ResourceCategoryList;

sealed class ResourceCategoryListEndpoint : EndpointHandlerBase<
    ResourceCategoryListApiRequest,
    ResourceCategoryListQuery,
    ResourceCategory[],
    ResourceCategoryListApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public ResourceCategoryListEndpoint(
        IPresentationMapper<ResourceCategoryListApiRequest, ResourceCategoryListQuery> requestMapper,
        IPresentationMapper<ResourceCategory[], ResourceCategoryListApiResponse[]> responseMapper)
        : base(
            Endpoints.Resource.ResourceCategories,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ResourceCategoryListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new ResourceCategoryListQuery(request.LastId)
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                    })),
            cancellationToken);

}
sealed class ResourceCategoryListApiRequestMapper : IPresentationMapper<
    ResourceCategoryListApiRequest,
    ResourceCategoryListQuery>
{
    public ValueTask<PrimitiveResult<ResourceCategoryListQuery>> Map(
        ResourceCategoryListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new ResourceCategoryListQuery(src.LastId)
                {
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize
                }));
    }
}
sealed class ResourceCategoryListApiResponseMapper : IPresentationMapper<
    ResourceCategory[],
    ResourceCategoryListApiResponse[]>
{
    public ValueTask<PrimitiveResult<ResourceCategoryListApiResponse[]>> Map(
        ResourceCategory[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    src.Select(data =>
                    new ResourceCategoryListApiResponse(
                        data.Id,
                        data.TableId,
                        data.Title,
                        data.Description,
                        data.Count,
                        data.ParentId,
                        data.CreationDate.ToPersianDate()))
                    .ToArray()));
    }
}