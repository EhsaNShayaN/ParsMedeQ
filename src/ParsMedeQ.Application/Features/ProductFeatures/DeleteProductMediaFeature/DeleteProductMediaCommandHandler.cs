namespace ParsMedeQ.Application.Features.ProductFeatures.DeleteProductMediaFeature;
public sealed class DeleteProductMediaCommandHandler : IPrimitiveResultCommandHandler<DeleteProductMediaCommand, DeleteProductMediaCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DeleteProductMediaCommandHandler(
        IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DeleteProductMediaCommandResponse>> Handle(DeleteProductMediaCommand request, CancellationToken cancellationToken)
        => await this._writeUnitOfWork.ProductWriteRepository.FindByIdWithMediaList(request.ProductId, cancellationToken)
              .Map(product => product.DeleteMedia(request.MediaId))
              .Map(ProductCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                  .Map(count => new DeleteProductMediaCommandResponse(count > 0)))
              .ConfigureAwait(false);
}
