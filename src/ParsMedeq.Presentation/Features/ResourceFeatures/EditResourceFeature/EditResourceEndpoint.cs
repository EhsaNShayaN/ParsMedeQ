using MediatR;
using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts;
using ParsMedeQ.Contracts.ResourceContracts.UpdateResourceContract;
using SRH.Utilities.EhsaN;
using System.Text.Json;
using System.Web;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.EditResourceFeature;
sealed class EditResourceEndpoint : EndpointHandlerBase<
    UpdateResourceApiRequest,
    UpdateResourceCommand,
    UpdateResourceCommandResponse,
    UpdateResourceApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public EditResourceEndpoint() : base(
            Endpoints.Resource.EditResource,
            HttpMethod.Post)
    { }
    protected override Delegate EndpointDelegate =>
        async (
        UpdateResourceApiRequest request,
        IFileService fileService,
        ISender sender,
        CancellationToken cancellationToken) =>
    {
        var imageInfo = await fileService.ReadStream(request.Image).ConfigureAwait(false);
        var fileInfo = await fileService.ReadStream(request.File).ConfigureAwait(false);

        var description = HttpUtility.HtmlDecode(request.Description ?? string.Empty);
        var anchors = string.Empty;
        if (!string.IsNullOrEmpty(request.Anchors))
        {
            var DefaultJsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var anchorsArray = JsonSerializer.Deserialize<AnchorInfo[]>(request.Anchors, DefaultJsonSerializerOptions);
            var counter = 1;
            foreach (var anchor in anchorsArray)
            {
                description += $"<div id='{anchor.Id}' #{anchor.Id}><h3>{counter++}. {anchor.Name}</h3><p>{HttpUtility.HtmlDecode(anchor.Desc)}</p></div>";
            }
            anchors = JsonSerializer.Serialize(anchorsArray);
        }

        var command = new UpdateResourceCommand(
             request.Id,
             request.TableId,
             request.Title,
             description,
             HttpUtility.HtmlDecode(request.Abstract),
             anchors,
             request.Keywords?.Replace("،", ","),
             request.ResourceCategoryId,
             request.Language,
             request.PublishDate,
             request.PublishInfo,
             request.Publisher,
             request.Price,
             request.Discount,
             string.IsNullOrEmpty(request.ExpirationDate) ? default : CreateExpirationDate(request.ExpirationDate, request.ExpirationTime),
             imageInfo,
             fileInfo,
             request.OldImagePath,
             request.OldFileId == 0 ? null : request.OldFileId);

        return await this.CallMediatRHandler(sender,
                () => ValueTask.FromResult(PrimitiveResult.Success(command)),
                cancellationToken)
            .ConfigureAwait(false);
    };

    private static DateTime? CreateExpirationDate(string expirationDate, string expirationTime)
    {
        DateTime? date = null;
        if (!string.IsNullOrEmpty(expirationDate))
        {
            date = expirationDate.ToGeorgianDate();
            if (!string.IsNullOrEmpty(expirationTime))
            {
                var array = expirationTime.Split(":");
                date = date.Value.AddHours(array[0].ToInt());
                date = date.Value.AddMinutes(array[1].ToInt());
            }
        }
        return date;
    }
}