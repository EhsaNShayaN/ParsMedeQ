namespace ParsMedeQ.Application.Features.SectionFeatures.UpdateSectionFeature;
public sealed class UpdateSectionCommandHandler : IPrimitiveResultCommandHandler<UpdateSectionCommand, UpdateSectionCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public UpdateSectionCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
        this._userLangContextAccessor = userLangContextAccessor;
    }

    public async Task<PrimitiveResult<UpdateSectionCommandResponse>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        return await _writeUnitOfWork.SectionWriteRepository.FindByTranslation(request.Id, cancellationToken)
            .Map(section => UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Sections", cancellationToken)
                .Map(path => (section, path: string.IsNullOrEmpty(path) ? request.OldImagePath : path))
            .Map(data => data.section.Update(_userLangContextAccessor.GetCurrentLang(), request.Title, request.Description, data.path)
            .Map(section => this._writeUnitOfWork.SectionWriteRepository.EditSection(section)
            .Map(section => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => section))
            .Map(section => new UpdateSectionCommandResponse(section is not null)))))
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
