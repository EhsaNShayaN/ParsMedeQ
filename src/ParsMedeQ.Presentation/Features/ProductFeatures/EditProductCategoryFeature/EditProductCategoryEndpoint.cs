using ParsMedeQ.Application.Features.ProductFeatures.UpdateProductCategoryFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.UpdateProductCategoryContract;

namespace ParsMedeQ.Presentation.Features.ProductFeatures.EditProductCategoryFeature;
sealed class EditProductCategoryEndpoint : EndpointHandlerBase<
    UpdateProductCategoryApiRequest,
    UpdateProductCategoryCommand,
    UpdateProductCategoryCommandResponse,
    UpdateProductCategoryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public EditProductCategoryEndpoint(
        IPresentationMapper<UpdateProductCategoryApiRequest, UpdateProductCategoryCommand> apiRequestMapper
        ) : base(
            Endpoints.Product.EditProductCategory,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class UpdateProductCategoryApiRequestMapper : IPresentationMapper<UpdateProductCategoryApiRequest, UpdateProductCategoryCommand>
{
    public async ValueTask<PrimitiveResult<UpdateProductCategoryCommand>> Map(UpdateProductCategoryApiRequest src, CancellationToken cancellationToken)
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
                new UpdateProductCategoryCommand(
                    src.Id,
                    src.Title,
                    src.Description,
                    src.ParentId,
                    imageBytes,
                    imageExtension,
                    src.OldImagePath)));
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