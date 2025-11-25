namespace ParsMedeQ.Application.Features.SectionFeatures.UpdateSectionByListFeature;
public sealed class UpdateSectionByListCommandHandler : IPrimitiveResultCommandHandler<UpdateSectionByListCommand, UpdateSectionByListCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;
    private readonly IUserLangContextAccessor _userLangContextAccessor;

    public UpdateSectionByListCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService,
        IUserLangContextAccessor userLangContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
        this._userLangContextAccessor = userLangContextAccessor;
    }

    public async Task<PrimitiveResult<UpdateSectionByListCommandResponse>> Handle(UpdateSectionByListCommand request, CancellationToken cancellationToken)
    {
        return await _writeUnitOfWork.SectionWriteRepository.FindByTranslation(request.Id, cancellationToken)
            .Map(section => section.RemoveTranslations(_userLangContextAccessor.GetCurrentLang()))
            .Map(section => section.AddTranslations(_userLangContextAccessor.GetCurrentLang(), request.Items.Select(s => ToTuple(s)).ToArray())
                .Map(() => section))
            .Map(section => this._writeUnitOfWork.SectionWriteRepository.EditSection(section)
            .Map(section => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => section))
            .Map(section => new UpdateSectionByListCommandResponse(section is not null)))
            .ConfigureAwait(false);
    }
    public static (string title, string description) ToTuple(UpdateSectionByListItemCommand cmd)
    => (cmd.Title, cmd.Description);
}
