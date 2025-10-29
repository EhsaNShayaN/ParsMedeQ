using ParsMedeQ.Application.Services.UserLangServices;
using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;

namespace ParsMedeQ.Application.Features.ProductFeatures.UpdateProductFeature;
public sealed class UpdateProductCommandHandler : IPrimitiveResultCommandHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public UpdateProductCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<UpdateProductCommandResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Media? defaultMedia = null;
        var langCode = _userLangContextAccessor.GetCurrentLang();
        return
            await this._writeUnitOfWork.ProductWriteRepository.FindById(request.Id, cancellationToken)

            .Map(Product => UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Images", cancellationToken)
                .Map(imagePath => (Product, imagePath)))
            .Map(data => UploadFile(this._fileService, request.FileInfo?.Bytes, request.FileInfo?.Extension, "Files", cancellationToken)
                .Map(filePath => (data.Product, data.imagePath, filePath)))
            .MapIf(
                data => string.IsNullOrEmpty(data.filePath),
                data => ValueTask.FromResult(PrimitiveResult.Success((data.Product, data.imagePath, data.filePath, media: defaultMedia))),
                data => Media.Create(request.TableId, data.filePath, request.FileInfo?.MimeType, request.FileInfo?.Name)
                    .Map(media => _writeUnitOfWork.MediaWriteRepository.AddMedia(media))
                    .Map(media => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                        .Map(_ => media))
                    .Map(media => (data.Product, data.imagePath, data.filePath, media)))

            .MapIf(
                _ => langCode.Equals(Constants.LangCode_Farsi, StringComparison.OrdinalIgnoreCase),
                data => data.Product.Update(
                    request.ProductCategoryId, request.Language, request.PublishDate, request.PublishInfo, request.Publisher,
                    request.Price, request.Discount, langCode, request.Title, request.Description,
                    request.Abstract, request.Anchors, request.Keywords, request.GuarantyExpirationTime, request.PeriodicServiceInterval,
                    data.imagePath ?? request.OldImagePath, data.media?.Id ?? request.OldFileId),
                data => data.Product.UpdateTranslation(langCode, request.Title, request.Description, request.Abstract, request.Anchors, request.Keywords,
                    data.imagePath ?? request.OldImagePath, data.media?.Id ?? request.OldFileId)
                    .Map(() => data.Product)
            )
            .Map(Product => this._writeUnitOfWork.ProductWriteRepository.UpdateProduct(Product, cancellationToken)
            .Map(Product => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => Product))
            .Map(Product => new UpdateProductCommandResponse(Product is not null)))
            .ConfigureAwait(false);
    }

    static async ValueTask<PrimitiveResult<string>> UploadFile(
        IFileService fileService,
        byte[] bytes,
        string fileExtension,
        string fodlerName,
        CancellationToken cancellationToken)
    {
        if ((bytes?.Length ?? 0) == 0) return null;
        var result = await fileService.UploadFile(bytes, fileExtension, fodlerName, cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(result)) return PrimitiveResult.Failure<string>("", "Can not upload file");

        return result;
    }
}
