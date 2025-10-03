using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;

namespace ParsMedeQ.Application.Features.ProductFeatures.AddProductMediaFeature;
public sealed class AddProductMediaCommandHandler : IPrimitiveResultCommandHandler<AddProductMediaCommand, AddProductMediaCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public AddProductMediaCommandHandler(
        IWriteUnitOfWork writeUnitOfWork, IFileService fileService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<AddProductMediaCommandResponse>> Handle(AddProductMediaCommand request, CancellationToken cancellationToken)
    {
        for (var i = 0; i< request.FilesArray.Length; i++)
        {
            var file = request.FilesArray[i];
            var extension = request.FileExtensions[i];
            UploadFile(this._fileService, file, extension, "Products", cancellationToken)
                .Map(path=> ProductMedia.Create())
            //await ProductMedia.Create(request.ProductId)
        }
        return await ProductMedia.Create(
            request.ParentId,
            DateTime.Now)
            .Map(resource => UploadFile(this._fileService, request.Image, request.ImageExtension, "Images", cancellationToken)
                .Map(imagePath => (resource, imagePath)))
            .Map(data => data.resource.AddTranslation(Constants.LangCode_Farsi.ToLower(), request.Title, request.Description, data.imagePath)
                .Map(() => data.resource))
            .Map(productMedia => this._writeUnitOfWork.ProductWriteRepository.AddProductMedia(productMedia, cancellationToken)
            .Map(productMedia => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => productMedia))
            .Map(productMedia => new AddProductMediaCommandResponse(productMedia is not null)))
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
