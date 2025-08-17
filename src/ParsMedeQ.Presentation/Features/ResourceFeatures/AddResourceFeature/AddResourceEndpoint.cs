using MediatR;
using ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ResourceContracts.AddResourceContract;
using SRH.Utilities.EhsaN;

namespace ParsMedeQ.Presentation.Features.ResourceFeatures.AddResourceFeature;
sealed class AddResourceEndpoint : EndpointHandlerBase<
    AddResourceApiRequest,
    AddResourceCommand,
    AddResourceCommandResponse,
    AddResourceApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddResourceEndpoint() : base(
            Endpoints.Resource.AddResource,
            HttpMethod.Post)
    { }
    protected override Delegate EndpointDelegate =>
        async (
        AddResourceApiRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
    {
        var imageBytes = await this.ReadStream(request.Image.OpenReadStream()).ConfigureAwait(false);
        var fileBytes = await this.ReadStream(request.File.OpenReadStream()).ConfigureAwait(false);

        var command = new AddResourceCommand(
             request.TableId,
             request.Title,
             request.Language,
             request.IsVip,
             request.Price,
             request.Discount,
             request.Description,
             request.PublishInfo,
             request.Publisher,
             request.ResourceCategoryId,
             request.ResourceCategoryTitle,
             request.Abstract,
             request.Anchors.Length > 0 ? Newtonsoft.Json.JsonConvert.SerializeObject(request.Anchors) : string.Empty,
             string.IsNullOrEmpty(request.ExpirationDate) ? default(DateTime?) : request.ExpirationDate.ToGeorgianDate(),
             request.ExpirationTime,
             request.Keywords,
             request.PublishDate,
             imageBytes,
             Path.GetExtension(request.Image.FileName),
             fileBytes,
             Path.GetExtension(request.File.FileName));

        return await this.CallMediatRHandler(sender,
                () => ValueTask.FromResult(PrimitiveResult.Success(command)),
                cancellationToken)
            .ConfigureAwait(false);
    };

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