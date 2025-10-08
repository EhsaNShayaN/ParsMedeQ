namespace ParsMedeQ.Application.Features.ResourceFeatures.DeleteResourceFeature;
public sealed class DeleteResourceCommandHandler : IPrimitiveResultCommandHandler<DeleteResourceCommand, DeleteResourceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DeleteResourceCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DeleteResourceCommandResponse>> Handle(DeleteResourceCommand request, CancellationToken cancellationToken) =>
        await _writeUnitOfWork.ResourceWriteRepository.FindById(request.Id, cancellationToken)
        .Map(resource => _writeUnitOfWork.ResourceWriteRepository.Delete(resource, cancellationToken))
        .Map(resource => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None))
        .Map(count => new DeleteResourceCommandResponse(count > 0))
        .ConfigureAwait(false);
}
