using MediatR;
using ParsMedeQ.Application.Features.ProductFeatures.UpdateProductFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.UpdateProductContract;
using ParsMedeQ.Contracts.ResourceContracts;
using SRH.Utilities.EhsaN;
using System.Text.Json;
using System.Web;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.EditProductFeature;
sealed class EditProductEndpoint : EndpointHandlerBase<
    UpdateProductApiRequest,
    UpdateProductCommand,
    UpdateProductCommandResponse,
    UpdateProductApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public EditProductEndpoint() : base(
            Endpoints.Product.EditProduct,
            HttpMethod.Post)
    { }
    protected override Delegate EndpointDelegate =>
        async (
        UpdateProductApiRequest request,
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

        var command = new UpdateProductCommand(
             request.Id,
             request.TableId,
             request.Title,
             description,
             HttpUtility.HtmlDecode(request.Abstract),
             anchors,
             request.Keywords?.Replace("،", ","),
             request.ProductCategoryId,
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
             fileExtension,
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