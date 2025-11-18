namespace ParsMedeQ.Application.Features.PaymentFeatures.ConfirmPaymentFeature;
public sealed class ConfirmPaymentCommandHandler : IPrimitiveResultCommandHandler<ConfirmPaymentCommand, ConfirmPaymentCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public ConfirmPaymentCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<ConfirmPaymentCommandResponse>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken) =>
        await _writeUnitOfWork.PaymentWriteRepository.FindPaymentWithDependencies(request.PaymentId, cancellationToken)
            .MapIf(
                payment => payment.Status > 0,
                payment => PrimitiveResult.Success(payment),
                payment =>
                {
                    PrimitiveResult.BindAll(payment.Order.OrderItems.Where(s => s.PeriodicServiceInterval > 0),
                        (orderItem) =>
                        orderItem.AddPeriodicService(DateTime.Now),
                        BindAllIterationStrategy.BreakOnFirstError)
                    .Map(periodicServices => this._writeUnitOfWork.OrderWriteRepository.AddPeriodicServices(periodicServices));


                    payment.ConfirmPayment(request.TransactionId)
                   .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.ConfirmPayment(payment).Map(_ => payment)
                   .Map(payment => this._writeUnitOfWork.OrderWriteRepository.PayOrder(payment.Order).Map(_ => payment)
                   .Map(_ => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment))));
                    return payment;
                })
            .Map(payment => new ConfirmPaymentCommandResponse(payment.TransactionId, payment.Id, payment.Order.OrderNumber, payment.Amount))
           .ConfigureAwait(false);
}
