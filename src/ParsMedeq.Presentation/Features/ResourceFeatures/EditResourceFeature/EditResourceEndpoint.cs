using MediatR;
using ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.UpdateResourceContract;
using SRH.Utilities.EhsaN;
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
            Endpoints.Resource.UpdateResource,
            HttpMethod.Post)
    { }
    protected override Delegate EndpointDelegate =>
        async (
        UpdateResourceApiRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
    {
        byte[] imageBytes = [];
        string imageExtension = string.Empty;
        byte[] fileBytes = [];
        string fileExtension = string.Empty;
        if (request.Image is not null)
        {
            imageBytes = await this.ReadStream(request.Image.OpenReadStream()).ConfigureAwait(false);
            imageExtension = Path.GetExtension(request.Image.FileName);
        }
        if (request.File is not null)
        {
            fileBytes = await this.ReadStream(request.File.OpenReadStream()).ConfigureAwait(false);
            fileExtension = Path.GetExtension(request.File.FileName);
        }

        var description = HttpUtility.HtmlDecode(request.Description ?? string.Empty);
        if (request.Anchors?.Length > 0)
        {
            var counter = 1;
            foreach (var anchor in request.Anchors)
            {
                description += $"<div id='{anchor.Id}' #{anchor.Id}><h3>{counter++}. {anchor.Name}</h3><p>{HttpUtility.HtmlDecode(anchor.Desc)}</p></div>";
            }
        }

        var command = new UpdateResourceCommand(
             request.Id,
             request.Title,
             description,
             HttpUtility.HtmlDecode(request.Abstract),
             request.Anchors?.Any() ?? false ? Newtonsoft.Json.JsonConvert.SerializeObject(request.Anchors) : string.Empty,
             request.Keywords?.Replace("،", ","),
             request.ResourceCategoryId,
             request.Language,
             request.PublishDate,
             request.PublishInfo,
             request.Publisher,
             request.Price,
             request.Discount,
             request.IsVip,
             string.IsNullOrEmpty(request.ExpirationDate) ? default : CreateExpirationDate(request.ExpirationDate, request.ExpirationTime),
             imageBytes,
             imageExtension,
             fileBytes,
             fileExtension);

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

    async ValueTask<byte[]> ReadStream(Stream stream)
    {
        // Ensure the stream is positioned at the beginning
        if (stream.CanSeek)
        {
            stream.Seek(0, SeekOrigin.Begin);
        }
        using (MemoryStream memoryStream = new MemoryStream())
        {
            byte[] buffer = new byte[8192]; // 8KB buffer
            int bytesRead;

            // Asynchronously read chunks from the input stream
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                // Write the read bytes to the memory stream
                await memoryStream.WriteAsync(buffer, 0, bytesRead);
            }
            // Return the accumulated bytes
            return memoryStream.ToArray();
        }
    }
}