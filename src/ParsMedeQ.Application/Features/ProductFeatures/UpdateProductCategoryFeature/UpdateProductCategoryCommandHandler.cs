namespace ParsMedeQ.Application.Features.ProductFeatures.UpdateProductCategoryFeature;
public sealed class UpdateProductCategoryCommandHandler : IPrimitiveResultCommandHandler<UpdateProductCategoryCommand, UpdateProductCategoryCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public UpdateProductCategoryCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<UpdateProductCategoryCommandResponse>> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var langCode = _userLangContextAccessor.GetCurrentLang();
        return
            await this._writeUnitOfWork.ProductWriteRepository.FindCategoryById(request.Id, cancellationToken)
            .Map(resource => UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Images", cancellationToken)
                .Map(imagePath => (resource, imagePath)))
            .MapIf(
                _ => langCode.Equals(Constants.LangCode_Farsi, StringComparison.OrdinalIgnoreCase),
                data => data.resource.Update(request.ParentId, langCode, request.Title, request.Description, data.imagePath ?? request.OldImagePath),
                data => data.resource.UpdateTranslation(langCode, request.Title, request.Description, data.imagePath ?? request.OldImagePath).Map(() => data.resource)
            )
            .Map(ProductCategory => this._writeUnitOfWork.ProductWriteRepository.UpdateProductCategory(ProductCategory, cancellationToken)
            .Map(ProductCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => ProductCategory))
            .Map(ProductCategory => new UpdateProductCategoryCommandResponse(ProductCategory is not null)))
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
