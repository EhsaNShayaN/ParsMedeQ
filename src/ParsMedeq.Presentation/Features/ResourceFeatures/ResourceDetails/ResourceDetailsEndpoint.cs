using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ParsMedeQ.Application.Features.ResourceFeatures.ResourceDetailsFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.AddResourceContract;
using ParsMedeQ.Contracts.ResourceContracts.ResourceDetailsContract;
using ParsMedeQ.Contracts.ResourceContracts.ResourceListContract;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.ResourceDetails;

sealed class ResourceDetailsEndpoint : EndpointHandlerBase<
    ResourceDetailsApiRequest,
    ResourceDetailsQuery,
    Resource,
    ResourceDetailsApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public ResourceDetailsEndpoint(
        IPresentationMapper<ResourceDetailsApiRequest, ResourceDetailsQuery> requestMapper,
        IPresentationMapper<Resource, ResourceDetailsApiResponse> responseMapper)
        : base(
            Endpoints.Resource.Resource,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ResourceDetailsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new ResourceDetailsQuery(0, request.Id, request.TableId))),
            cancellationToken);

}
sealed class ResourceDetailsApiRequestMapper : IPresentationMapper<
    ResourceDetailsApiRequest,
    ResourceDetailsQuery>
{
    public ValueTask<PrimitiveResult<ResourceDetailsQuery>> Map(
        ResourceDetailsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new ResourceDetailsQuery(0, src.Id, src.TableId)));
    }
}
sealed class ResourceDetailsApiResponseMapper : IPresentationMapper<
    Resource,
    ResourceDetailsApiResponse>
{
    public ValueTask<PrimitiveResult<ResourceDetailsApiResponse>> Map(
        Resource src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new ResourceDetailsApiResponse(
                        src.Id,
                        src.TableId,
                        src.ResourceCategoryId,
                        src.ResourceCategoryTitle,
                        src.Title,
                        src.Abstract,
                        JsonConvert.DeserializeObject<AnchorInfo[]>(src.Anchors),
                        src.Description,
                        src.Keywords,
                        src.Image,
                        src.MimeType,
                        src.Doc,
                        src.Language,
                        src.PublishDate,
                        src.PublishInfo,
                        src.Publisher,
                        src.Price,
                        src.Discount,
                        src.IsVip,
                        src.DownloadCount,
                        src.Ordinal,
                        src.Deleted,
                        src.Disabled,
                        src.ExpirationDate.ToPersianDate(),
                        src.ExpirationDate.HasValue ? $"{src.ExpirationDate.Value.Hour}:{src.ExpirationDate.Value.Minute}" : null,
                        src.ExpirationDate.HasValue && src.ExpirationDate.Value < DateTime.Now,
                        src.CreationDate.ToPersianDate(),
                        false)
                    ));
    }
}