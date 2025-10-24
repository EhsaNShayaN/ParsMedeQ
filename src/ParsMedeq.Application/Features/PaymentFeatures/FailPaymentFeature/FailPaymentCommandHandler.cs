namespace ParsMedeQ.Application.Features.PaymentFeatures.FailPaymentFeature;
public sealed class FailPaymentCommandHandler : IPrimitiveResultCommandHandler<FailPaymentCommand, FailPaymentCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public FailPaymentCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<FailPaymentCommandResponse>> Handle(FailPaymentCommand request, CancellationToken cancellationToken) =>
    await _writeUnitOfWork.PaymentWriteRepository.FindByIdWithOrder(request.PaymentId, cancellationToken)
            .MapIf(
                payment => payment.Status > 0,
                payment => PrimitiveResult.Success(payment),
                payment =>
                {
                    payment.FailPayment()
                    .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.FailPayment(payment)
                    .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment)));
                    return payment;
                })
            .Map(payment => new FailPaymentCommandResponse(payment is not null))
           .ConfigureAwait(false);
}
