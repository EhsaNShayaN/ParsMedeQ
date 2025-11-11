namespace ParsMedeQ.Application.Features.ProductFeatures.DonePeriodicServiceFeature;
public sealed class DonePeriodicServiceCommandHandler : IPrimitiveResultCommandHandler<DonePeriodicServiceCommand, DonePeriodicServiceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DonePeriodicServiceCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DonePeriodicServiceCommandResponse>> Handle(DonePeriodicServiceCommand request, CancellationToken cancellationToken)
    {
        return await this._writeUnitOfWork.ProductWriteRepository.FindByPeriodicService(request.ProductId, request.Id, cancellationToken)
              .Map(product => product.DonePeriodicService(request.Id))
              .Map(ProductCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                  .Map(count => new DonePeriodicServiceCommandResponse(count > 0)))
              .ConfigureAwait(false);
    }
}
