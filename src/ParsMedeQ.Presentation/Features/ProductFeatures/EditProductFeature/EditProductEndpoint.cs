using MediatR;
using ParsMedeQ.Application;
using ParsMedeQ.Application.Features.ProductFeatures.UpdateProductFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.ProductContracts.UpdateProductContract;
using ParsMedeQ.Contracts.ResourceContracts;
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
                 request.GuarantyExpirationTime,
                 request.PeriodicServiceInterval,
                 imageInfo,
                 fileInfo,
                 request.OldImagePath,
                 request.OldFileId == 0 ? null : request.OldFileId);

            return await this.CallMediatRHandler(sender,
                    () => ValueTask.FromResult(PrimitiveResult.Success(command)),
                    cancellationToken)
                .ConfigureAwait(false);
        };
}