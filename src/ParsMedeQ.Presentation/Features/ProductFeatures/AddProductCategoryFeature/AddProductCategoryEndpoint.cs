using ParsMedeQ.Application.Features.ProductFeatures.AddProductCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.AddProductCategoryContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.AddProductCategoryFeature;
sealed class AddProductCategoryEndpoint : EndpointHandlerBase<
    AddProductCategoryApiRequest,
    AddProductCategoryCommand,
    AddProductCategoryCommandResponse,
    AddProductCategoryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddProductCategoryEndpoint(
        IPresentationMapper<AddProductCategoryApiRequest, AddProductCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.AddProductCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddProductCategoryApiRequestMapper : IPresentationMapper<AddProductCategoryApiRequest, AddProductCategoryCommand>
{
    public async ValueTask<PrimitiveResult<AddProductCategoryCommand>> Map(AddProductCategoryApiRequest src, CancellationToken cancellationToken)
    {
        byte[] imageBytes = [];
        string imageExtension = string.Empty;
        if (src.Image is not null)
        {
            imageBytes = await this.ReadStream(src.Image.OpenReadStream()).ConfigureAwait(false);
            imageExtension = Path.GetExtension(src.Image.FileName);
        }
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddProductCategoryCommand(
                    src.TableId,
                    src.Title,
                    src.Description,
                    src.ParentId,
                    imageBytes,
                    imageExtension)));
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