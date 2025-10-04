using ParsMedeQ.Application.Services.UserLangServices;
using ParsMedeQ.Domain;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;

namespace ParsMedeQ.Application.Features.ResourceFeatures.UpdateResourceFeature;
public sealed class UpdateResourceCommandHandler : IPrimitiveResultCommandHandler<UpdateResourceCommand, UpdateResourceCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public UpdateResourceCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<UpdateResourceCommandResponse>> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
    {
        Media? defaultMedia = null;
        var langCode = _userLangContextAccessor.GetCurrentLang();
        return
            await this._writeUnitOfWork.ResourceWriteRepository.FindById(request.Id, cancellationToken)

            .Map(resource => UploadFile(this._fileService, request.Image, request.ImageExtension, "Images", cancellationToken)
                .Map(imagePath => (resource, imagePath)))
            .Map(data => UploadFile(this._fileService, request.File, request.FileExtension, "Files", cancellationToken)
                .Map(filePath => (data.resource, data.imagePath, filePath)))
            .MapIf(
                data => string.IsNullOrEmpty(data.filePath),
                data => ValueTask.FromResult(PrimitiveResult.Success((data.resource, data.imagePath, data.filePath, media: defaultMedia))),
                data => Media.Create(request.TableId, data.filePath, string.Empty)
                    .Map(media => _writeUnitOfWork.MediaWriteRepository.AddMedia(media))
                    .Map(media => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                        .Map(_ => media))
                    .Map(media => (data.resource, data.imagePath, data.filePath, media)))

            .MapIf(
                _ => langCode.Equals(Constants.LangCode_Farsi, StringComparison.OrdinalIgnoreCase),
                data => data.resource.Update(
                    request.ResourceCategoryId, request.Language, request.PublishDate, request.PublishInfo, request.Publisher,
                    request.Price, request.Discount, request.ExpirationDate, langCode, request.Title, request.Description,
                    request.Abstract, request.Anchors, request.Keywords, data.imagePath ?? request.OldImagePath, data.media?.Id ?? request.OldFileId),
                data => data.resource.UpdateTranslation(langCode, request.Title, request.Description, request.Abstract, request.Anchors, request.Keywords,
                    data.imagePath ?? request.OldImagePath, data.media?.Id ?? request.OldFileId)
                    .Map(() => data.resource)
            )
            .Map(resource => this._writeUnitOfWork.ResourceWriteRepository.UpdateResource(resource, cancellationToken)
            .Map(resource => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => resource))
            .Map(resource => new UpdateResourceCommandResponse(resource is not null)))
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
