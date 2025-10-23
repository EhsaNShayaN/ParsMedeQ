namespace ParsMedeQ.Application.Features.PaymentFeatures.ConfirmPaymentFeature;
public sealed class ConfirmPaymentCommandHandler : IPrimitiveResultCommandHandler<ConfirmPaymentCommand, ConfirmPaymentCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public ConfirmPaymentCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<ConfirmPaymentCommandResponse>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
        return await _writeUnitOfWork.PaymentWriteRepository.FindById(request.PaymentId, cancellationToken)
           .Map(payment => payment.ConfirmPayment(request.TransactionId))
           .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.ConfirmPayment(payment)
           .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment))
           .Map(payment => new ConfirmPaymentCommandResponse(payment is not null)))
           .ConfigureAwait(false);
    }
}
