using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductCategoryFeature;
public sealed class AddProductCategoryCommandHandler : IPrimitiveResultCommandHandler<AddProductCategoryCommand, AddProductCategoryCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public AddProductCategoryCommandHandler(
        IWriteUnitOfWork writeUnitOfWork, IFileService fileService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<AddProductCategoryCommandResponse>> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
    {
        return await ProductCategory.Create(
            request.ParentId,
            DateTime.Now)
            .Map(resource => UploadFile(this._fileService, request.Image, request.ImageExtension, "Images", cancellationToken)
                .Map(imagePath => (resource, imagePath)))
            .Map(data => data.resource.AddTranslation(Constants.LangCode_Farsi.ToLower(), request.Title, request.Description, data.imagePath)
                .Map(() => data.resource))
            .Map(ProductCategory => this._writeUnitOfWork.ProductWriteRepository.AddProductCategory(ProductCategory, cancellationToken)
            .Map(ProductCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => ProductCategory))
            .Map(ProductCategory => new AddProductCategoryCommandResponse(ProductCategory is not null)))
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
