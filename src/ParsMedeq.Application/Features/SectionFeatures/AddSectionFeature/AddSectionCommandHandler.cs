using ParsMedeQ.Domain.Aggregates.SectionAggregate;

namespace ParsMedeQ.Application.Features.SectionFeatures.AddSectionFeature;
public sealed class AddSectionCommandHandler : IPrimitiveResultCommandHandler<AddSectionCommand, AddSectionCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public AddSectionCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
        this._userLangContextAccessor = userLangContextAccessor;
    }

    public async Task<PrimitiveResult<AddSectionCommandResponse>> Handle(AddSectionCommand request, CancellationToken cancellationToken)
    {
        return await UploadFile(this._fileService, request.ImageInfo?.Bytes, request.ImageInfo?.Extension, "Sections", cancellationToken)
            .Map(path => Section.Create()
                .Map(section => (path, section))
            .Map(data => data.section.AddTranslation(
                _userLangContextAccessor.GetCurrentLang(),
                request.Title,
                request.Description,
                data.path).Map(() => data.section))
            .Map(section => this._writeUnitOfWork.SectionWriteRepository.AddSection(section)
            .Map(section => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => section))
            .Map(section => new AddSectionCommandResponse(section is not null))))
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
