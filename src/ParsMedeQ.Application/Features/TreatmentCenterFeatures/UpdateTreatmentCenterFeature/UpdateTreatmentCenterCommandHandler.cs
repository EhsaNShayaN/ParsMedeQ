namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.UpdateTreatmentCenterFeature;
public sealed class UpdateTreatmentCenterCommandHandler : IPrimitiveResultCommandHandler<UpdateTreatmentCenterCommand, UpdateTreatmentCenterCommandResponse>
{
    private readonly IUserLangContextAccessor _userLangContextAccessor;
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public UpdateTreatmentCenterCommandHandler(
        IUserLangContextAccessor userLangContextAccessor,
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {
        this._userLangContextAccessor = userLangContextAccessor;
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }

    public async Task<PrimitiveResult<UpdateTreatmentCenterCommandResponse>> Handle(UpdateTreatmentCenterCommand request, CancellationToken cancellationToken)
    {
        var langCode = _userLangContextAccessor.GetCurrentLang();
        return
            await this._writeUnitOfWork.TreatmentCenterWriteRepository.FindById(request.Id, cancellationToken)
            .Map(center => UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Images", cancellationToken)
                .Map(imagePath => (center, imagePath)))
            .MapIf(
                _ => langCode.Equals(Constants.LangCode_Farsi, StringComparison.OrdinalIgnoreCase),
                data => data.center.Update(
                    request.ProvinceId,
                    request.CityId,
                    langCode,
                    request.Title,
                    request.Description,
                    data.imagePath ?? request.OldImagePath),
                data => data.center.UpdateTranslation(
                    langCode,
                request.Title,
                request.Description,
                data.imagePath ?? request.OldImagePath)
                .Map(() => data.center))
            .Map(TreatmentCenter => this._writeUnitOfWork.TreatmentCenterWriteRepository.UpdateTreatmentCenter(TreatmentCenter, cancellationToken)
            .Map(TreatmentCenter => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => TreatmentCenter))
            .Map(TreatmentCenter => new UpdateTreatmentCenterCommandResponse(TreatmentCenter is not null)))
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
