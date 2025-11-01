namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.DeleteTreatmentCenterFeature;
public sealed class DeleteTreatmentCenterCommandHandler : IPrimitiveResultCommandHandler<DeleteTreatmentCenterCommand, DeleteTreatmentCenterCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DeleteTreatmentCenterCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DeleteTreatmentCenterCommandResponse>> Handle(DeleteTreatmentCenterCommand request, CancellationToken cancellationToken) =>
        await _writeUnitOfWork.TreatmentCenterWriteRepository.FindById(request.Id, cancellationToken)
        .Map(center => _writeUnitOfWork.TreatmentCenterWriteRepository.DeleteTreatmentCenter(center)
        .Map(category => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None))
        .Map(count => new DeleteTreatmentCenterCommandResponse(count > 0)))
        .ConfigureAwait(false);
}
