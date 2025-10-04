using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;

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
        return await this._writeUnitOfWork.ProductWriteRepository.FindByIdWithMediaList(request.ProductId, cancellationToken)
            .Map(product =>
                PrimitiveResult.BindAll(request.FilesArray, (file, itemIndex) =>
                    UploadFile(this._fileService, file, request.FileExtensions[itemIndex], "Products", cancellationToken)
                    .Map(path => Media.Create(Tables.Product.GetHashCode(), path, string.Empty))
                    .Map(media => _writeUnitOfWork.MediaWriteRepository.AddMedia(media)),
                    BindAllIterationStrategy.BreakOnFirstError)
                .Map(medias => _writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => medias))
                .Map(medias => product.AddMediaList(medias.Select(m => m.Id).ToArray()).Map(() => medias)))
            .Map(medias => _writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(affectedRows => new AddProductMediaCommandResponse(affectedRows > 0)))
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
