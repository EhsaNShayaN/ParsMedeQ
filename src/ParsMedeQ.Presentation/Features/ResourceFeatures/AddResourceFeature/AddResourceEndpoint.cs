using org.apache.zookeeper.data;
using ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.AddResourceContracts;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using SRH.Utilities.EhsaN;
using System.Security.Policy;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.AddResourceFeature;
sealed class AddResourceEndpoint : EndpointHandlerBase<
    AddResourceApiRequest,
    AddResourceCommand,
    AddResourceCommandResponse,
    AddResourceApiResponse>
{
    protected override bool NeedTaxPayerAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddResourceEndpoint(
        IPresentationMapper<AddResourceApiRequest, AddResourceCommand> apiRequestMapper
        ) : base(
            Endpoints.Resource.AddResource,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddResourceApiRequestMapper : IPresentationMapper<AddResourceApiRequest, AddResourceCommand>
{
    public ValueTask<PrimitiveResult<AddResourceCommand>> Map(AddResourceApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddResourceCommand(
                    src.Id,
                    src.TableId,
                    src.Title,
                    src.Abstract,
                    src.Anchors,
                    src.Description,
                    src.Keywords,
                    src.ResourceCategoryId,
                    src.ResourceCategoryTitle,
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
                    src.ExpirationDate,
                    src.CreationDate,
                    )));
}
                    /*src.Anchors.Length > 0 ? Newtonsoft.Json.JsonConvert.SerializeObject(src.Anchors) : string.Empty,
                    string.IsNullOrEmpty(src.ExpirationDate) ? default(DateTime?) : src.ExpirationDate.ToGeorgianDate(),*/