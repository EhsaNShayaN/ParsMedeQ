namespace ParsMedeQ.Application.Features.SectionFeatures.ShowSectionFeature;
public sealed class ShowSectionCommandHandler : IPrimitiveResultCommandHandler<ShowSectionCommand, ShowSectionCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public ShowSectionCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<ShowSectionCommandResponse>> Handle(ShowSectionCommand request, CancellationToken cancellationToken)
    {
        return await _writeUnitOfWork.SectionWriteRepository.FindById(request.Id, cancellationToken)
            .Map(section => section.Show())
            .Map(section => this._writeUnitOfWork.SectionWriteRepository.EditSection(section)
            .Map(section => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => section))
            .Map(section => new ShowSectionCommandResponse(section is not null)))
            .ConfigureAwait(false);
    }
}
