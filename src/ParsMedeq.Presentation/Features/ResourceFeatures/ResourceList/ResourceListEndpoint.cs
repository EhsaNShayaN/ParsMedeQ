using MediatR;
using Microsoft.AspNetCore.Http;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.ResourceListContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.ResourceList;

sealed class ResourceListEndpoint : EndpointHandlerBase<
    ResourceListApiRequest,
    ResourceListQuery,
    BasePaginatedApiResponse<ResourceListDbQueryResponse>,
    BasePaginatedApiResponse<ResourceListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public ResourceListEndpoint(
        IPresentationMapper<ResourceListApiRequest, ResourceListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<ResourceListDbQueryResponse>, BasePaginatedApiResponse<ResourceListApiResponse>> responseMapper)
        : base(
            Endpoints.Resource.Resources,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ResourceListApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(
                PrimitiveResult.Success(
                    new ResourceListQuery(request.TableId, request.ResourceCategoryId)
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                    })),
            cancellationToken);

}
sealed class ResourceListApiRequestMapper : IPresentationMapper<
    ResourceListApiRequest,
    ResourceListQuery>
{
    public ValueTask<PrimitiveResult<ResourceListQuery>> Map(
        ResourceListApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                new ResourceListQuery(src.TableId, src.ResourceCategoryId)
                {
                    TableId = src.TableId,
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
    }
}
sealed class ResourceListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<ResourceListDbQueryResponse>,
    BasePaginatedApiResponse<ResourceListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<ResourceListApiResponse>>> Map(
        BasePaginatedApiResponse<ResourceListDbQueryResponse> src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new BasePaginatedApiResponse<ResourceListApiResponse>(src.Items.Select(data =>
                    new ResourceListApiResponse(
                        data.Id,
                        data.TableId,
                        data.ResourceCategoryId,
                        data.ResourceCategoryTitle,
                        data.Title,
                        data.Image,
                        data.Language,
                        data.Price,
                        data.Discount,
                        data.IsVip,
                        data.DownloadCount,
                        data.Ordinal,
                        data.ExpirationDate.HasValue && data.ExpirationDate.Value < DateTime.Now,
                        data.CreationDate.ToPersianDate()))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}