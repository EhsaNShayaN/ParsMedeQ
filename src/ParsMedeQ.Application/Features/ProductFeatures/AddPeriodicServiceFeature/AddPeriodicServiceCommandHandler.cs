namespace ParsMedeQ.Application.Features.ProductFeatures.AddPeriodicServiceFeature;
public sealed class AddPeriodicServiceCommandHandler : IPrimitiveResultCommandHandler<AddPeriodicServiceCommand, AddPeriodicServiceCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddPeriodicServiceCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddPeriodicServiceCommandResponse>> Handle(AddPeriodicServiceCommand request, CancellationToken cancellationToken)
    {
        return await this._writeUnitOfWork.ProductWriteRepository.FindByPeriodicService(request.ProductId, request.Id, cancellationToken)
              .Map(product =>
                product.AddPeriodicService(product.PeriodicServices.First().UserId, product.PeriodicServices.First().ServiceDate))
              .Map(ProductCategory => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None)
                  .Map(count => new AddPeriodicServiceCommandResponse(count > 0)))
              .ConfigureAwait(false);
    }
}
