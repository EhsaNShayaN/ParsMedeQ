using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductFeature;
public sealed class AddProductCommandHandler : IPrimitiveResultCommandHandler<AddProductCommand, AddProductCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public AddProductCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<AddProductCommandResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        Media? defaultMedia = null;
        return await Product.Create(
            request.TableId,
            request.ProductCategoryId,
            request.Language,
            request.PublishDate,
            request.PublishInfo,
            request.Publisher,
            request.Price,
            request.Discount,
            request.IsVip,
            request.ExpirationDate)
            .Map(Product => UploadFile(this._fileService, request.Image, request.ImageExtension, "Images", cancellationToken)
                .Map(imagePath => (Product, imagePath)))
            .Map(data => UploadFile(this._fileService, request.File, request.FileExtension, "Files", cancellationToken)
                .Map(filePath => (data.Product, data.imagePath, filePath)))
            .MapIf(
                data => string.IsNullOrEmpty(data.filePath),
                data => ValueTask.FromResult(PrimitiveResult.Success((data.Product, data.imagePath, data.filePath, media: defaultMedia))),
                data => Media.Create(request.TableId, data.filePath, string.Empty)
                    .Map(media => _writeUnitOfWork.MediaWriteRepository.AddMedia(media, cancellationToken))
                    .Map(media => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                        .Map(_ => media))
                    .Map(media => (data.Product, data.imagePath, data.filePath, media)))
            .Map(data => data.Product.AddTranslation(
                    Constants.LangCode_Farsi.ToLower(), request.Title, request.Description,
                    request.Abstract, request.Anchors, request.Keywords, data.imagePath, data.media?.Id)
                .Map(() => data.Product))
            .Map(Product => _writeUnitOfWork.ProductWriteRepository.AddProduct(Product, cancellationToken))
            .Map(Product => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                .Map(_ => Product))
            .Map(Product => new AddProductCommandResponse(Product is not null))
            .ConfigureAwait(false);
    }

    static async ValueTask<PrimitiveResult<string>> UploadFile(
        IFileService fileService,
        byte[] bytes,
        string fileExtension,
        string fodlerName,
        CancellationToken cancellationToken)
    {
        if ((bytes?.Length ?? 0) == 0) return string.Empty;
        var result = await fileService.UploadFile(bytes, fileExtension, fodlerName, cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(result)) return PrimitiveResult.Failure<string>("", "Can not upload file");

        return result;
    }
}
