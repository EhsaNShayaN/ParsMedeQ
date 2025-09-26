using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;

namespace ParsMedeQ.Application.Features.ResourceFeatures.AddResourceFeature;
public sealed class AddResourceCommandHandler : IPrimitiveResultCommandHandler<AddResourceCommand, AddResourceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public AddResourceCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<AddResourceCommandResponse>> Handle(AddResourceCommand request, CancellationToken cancellationToken)
    {
        Media? defaultMedia = null;
        return await Resource.Create(
            request.TableId,
            request.ResourceCategoryId,
            request.Language,
            request.PublishDate,
            request.PublishInfo,
            request.Publisher,
            request.Price,
            request.Discount,
            request.ExpirationDate)
            .Map(resource => UploadFile(this._fileService, request.Image, request.ImageExtension, "Images", cancellationToken)
                .Map(imagePath => (resource, imagePath)))
            .Map(data => UploadFile(this._fileService, request.File, request.FileExtension, "Files", cancellationToken)
                .Map(filePath => (data.resource, data.imagePath, filePath)))
            .MapIf(
                data => string.IsNullOrEmpty(data.filePath),
                data => ValueTask.FromResult(PrimitiveResult.Success((data.resource, data.imagePath, data.filePath, media: defaultMedia))),
                data => Media.Create(request.TableId, data.filePath, string.Empty)
                    .Map(media => _writeUnitOfWork.MediaWriteRepository.AddMedia(media, cancellationToken))
                    .Map(media => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                        .Map(_ => media))
                    .Map(media => (data.resource, data.imagePath, data.filePath, media)))
            .Map(data => data.resource.AddTranslation(
                    Constants.LangCode_Farsi.ToLower(), request.Title, request.Description,
                    request.Abstract, request.Anchors, request.Keywords, data.imagePath, data.media?.Id)
                .Map(() => data.resource))
            .Map(resource => _writeUnitOfWork.ResourceWriteRepository.AddResource(resource, cancellationToken))
            .Map(resource => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                .Map(_ => resource))
            .Map(resource => new AddResourceCommandResponse(resource is not null))
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
