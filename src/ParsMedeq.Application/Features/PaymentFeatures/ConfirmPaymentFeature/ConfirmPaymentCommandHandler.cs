namespace ParsMedeQ.Application.Features.PaymentFeatures.ConfirmPaymentFeature;
public sealed class ConfirmPaymentCommandHandler : IPrimitiveResultCommandHandler<ConfirmPaymentCommand, ConfirmPaymentCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public ConfirmPaymentCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<ConfirmPaymentCommandResponse>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken) =>
        await _writeUnitOfWork.PaymentWriteRepository.FindByIdWithOrder(request.PaymentId, cancellationToken)
            .MapIf(
                payment => payment.Status > 0,
                payment => PrimitiveResult.Success(payment),
                payment =>
                {
                    payment.ConfirmPayment(request.TransactionId)
                    .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.ConfirmPayment(payment)
                    .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment)));
                    return payment;
                })
            .Map(payment => new ConfirmPaymentCommandResponse(payment.TransactionId, payment.OrderId, payment.Order.OrderNumber, payment.Amount))
           .ConfigureAwait(false);
}
