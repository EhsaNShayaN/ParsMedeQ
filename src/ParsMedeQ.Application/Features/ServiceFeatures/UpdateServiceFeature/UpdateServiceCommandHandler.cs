namespace ParsMedeQ.Application.Features.ServiceFeatures.UpdateServiceFeature;
public sealed class UpdateServiceCommandHandler : IPrimitiveResultCommandHandler<UpdateServiceCommand, UpdateServiceCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public UpdateServiceCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<UpdateServiceCommandResponse>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var langCode = _userLangContextAccessor.GetCurrentLang();
        return
            await this._writeUnitOfWork.ServiceWriteRepository.FindById(request.Id, cancellationToken)
            .Map(service => UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Images", cancellationToken)
                .Map(imagePath => (service, imagePath)))
            .MapIf(
                _ => langCode.Equals(Constants.LangCode_Farsi, StringComparison.OrdinalIgnoreCase),
                data => data.service.Update(
                langCode, request.Title, request.Description, data.imagePath ?? request.OldImagePath),
                data => data.service.UpdateTranslation(
                    langCode,
                    request.Title,
                    request.Description,
                data.imagePath ?? request.OldImagePath).Map(() => data.service)
            )
            .Map(Service => this._writeUnitOfWork.ServiceWriteRepository.UpdateService(Service, cancellationToken)
            .Map(Service => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => Service))
            .Map(Service => new UpdateServiceCommandResponse(Service is not null)))
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
