namespace ParsMedeQ.Application.Features.OrderFeatures.AddPeriodicServiceFeature;
public sealed class AddPeriodicServiceCommandHandler : IPrimitiveResultCommandHandler<AddPeriodicServiceCommand, AddPeriodicServiceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddPeriodicServiceCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddPeriodicServiceCommandResponse>> Handle(AddPeriodicServiceCommand request, CancellationToken cancellationToken)
    {
        return await this._writeUnitOfWork.OrderWriteRepository.FindByDependencies(request.OrderId, cancellationToken)
              .Map(order =>
              {
                  var orderItem = order.OrderItems.FirstOrDefault(s => s.Id == request.OrderItemId);
                  var periodicService = orderItem.PeriodicServices.FirstOrDefault(s => s.Id == request.PeriodicServiceId);
                  orderItem.AddPeriodicService(periodicService.ServiceDate);
                  return order;
              })
              .Map(_ => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                  .Map(count => new AddPeriodicServiceCommandResponse(count > 0)))
              .ConfigureAwait(false);
    }
}
