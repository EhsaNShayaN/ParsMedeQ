namespace ParsMedeQ.Application.Features.SectionFeatures.HideSectionFeature;
public sealed class HideSectionCommandHandler : IPrimitiveResultCommandHandler<HideSectionCommand, HideSectionCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public HideSectionCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<HideSectionCommandResponse>> Handle(HideSectionCommand request, CancellationToken cancellationToken)
    {
        return await _writeUnitOfWork.SectionWriteRepository.FindById(request.Id, cancellationToken)
            .Map(section => section.Hide())
            .Map(section => this._writeUnitOfWork.SectionWriteRepository.EditSection(section)
            .Map(section => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => section))
            .Map(section => new HideSectionCommandResponse(section is not null)))
            .ConfigureAwait(false);
    }
}
