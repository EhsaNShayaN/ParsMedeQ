namespace ParsMedeQ.Application.Features.ProductFeatures.DeleteProductFeature;
public sealed class DeleteProductCommandHandler : IPrimitiveResultCommandHandler<DeleteProductCommand, DeleteProductCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DeleteProductCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DeleteProductCommandResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        await _writeUnitOfWork.ProductWriteRepository.FindById(request.Id, cancellationToken)
        .Map(product => _writeUnitOfWork.ProductWriteRepository.Delete(product, cancellationToken))
        .Map(product => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None))
        .Map(count => new DeleteProductCommandResponse(count > 0))
        .ConfigureAwait(false);
}
