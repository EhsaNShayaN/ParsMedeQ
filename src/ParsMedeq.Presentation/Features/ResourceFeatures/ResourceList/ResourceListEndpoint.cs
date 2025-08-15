using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.AddResourceContract;
using ParsMedeQ.Contracts.ResourceContracts.ResourceListContract;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.ResourceList;

sealed class ResourceListEndpoint : EndpointHandlerBase<
    ResourceListApiRequest,
    ResourceListQuery,
    BasePaginatedApiResponse<Resource>,
    BasePaginatedApiResponse<ResourceListApiResponse>>
{
    protected override bool NeedTaxPayerFile => true;

    public ResourceListEndpoint(
        IPresentationMapper<ResourceListApiRequest, ResourceListQuery> requestMapper,
        IPresentationMapper<BasePaginatedApiResponse<Resource>, BasePaginatedApiResponse<ResourceListApiResponse>> responseMapper)
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
                    new ResourceListQuery(request.LastId)
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
        var x = ValueTask.FromResult(
            PrimitiveResult.Success(
                new ResourceListQuery(src.TableId)
                {
                    TableId = src.TableId,
                    PageIndex = src.PageIndex,
                    PageSize = src.PageSize,
                    LastId = src.LastId,
                }));
        return x;
    }
}
sealed class ResourceListApiResponseMapper : IPresentationMapper<
    BasePaginatedApiResponse<Resource>,
    BasePaginatedApiResponse<ResourceListApiResponse>>
{
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<ResourceListApiResponse>>> Map(
        BasePaginatedApiResponse<Resource> src,
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
                        data.Abstract,
                        JsonConvert.DeserializeObject<AnchorInfo[]>(data.Anchors),
                        data.Description,
                        data.Keywords,
                        data.Image,
                        data.MimeType,
                        data.Doc,
                        data.Language,
                        data.PublishDate,
                        data.PublishInfo,
                        data.Publisher,
                        data.Price,
                        data.Discount,
                        data.IsVip,
                        data.DownloadCount,
                        data.Ordinal,
                        data.Deleted,
                        data.Disabled,
                        data.ExpirationDate.ToPersianDate(),
                        data.ExpirationDate.HasValue ? $"{data.ExpirationDate.Value.Hour}:{data.ExpirationDate.Value.Minute}" : null,
                        data.ExpirationDate.HasValue && data.ExpirationDate.Value < DateTime.Now,
                        data.CreationDate.ToPersianDate(),
                        false))
                    .ToArray(), src.TotalCount, src.PageIndex, src.PageSize)
                    {
                        LastId = src.LastId
                    }));
    }
}