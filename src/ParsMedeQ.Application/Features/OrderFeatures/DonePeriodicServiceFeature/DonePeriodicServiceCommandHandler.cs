namespace ParsMedeQ.Application.Features.OrderFeatures.DonePeriodicServiceFeature;
public sealed class DonePeriodicServiceCommandHandler : IPrimitiveResultCommandHandler<DonePeriodicServiceCommand, DonePeriodicServiceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public DonePeriodicServiceCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<DonePeriodicServiceCommandResponse>> Handle(DonePeriodicServiceCommand request, CancellationToken cancellationToken)
    {
        return await this._writeUnitOfWork.OrderWriteRepository.FindByDependencies(request.OrderId, cancellationToken)
              .Map(order => order.DonePeriodicService(request.OrderItemId, request.PeriodicServiceId).Map(() => order))
              .Map(_ => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                  .Map(count => new DonePeriodicServiceCommandResponse(count > 0)))
              .ConfigureAwait(false);
    }
}
