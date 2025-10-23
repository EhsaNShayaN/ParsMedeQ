using ParsMedeQ.Domain.Aggregates.PaymentAggregate;

namespace ParsMedeQ.Application.Features.PaymentFeatures.FailPaymentFeature;
public sealed class FailPaymentCommandHandler : IPrimitiveResultCommandHandler<FailPaymentCommand, FailPaymentCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public FailPaymentCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<FailPaymentCommandResponse>> Handle(FailPaymentCommand request, CancellationToken cancellationToken)
    {
        return await _writeUnitOfWork.PaymentWriteRepository.FindById(request.PaymentId, cancellationToken)
            .Map(payment => payment.FailPayment())
            .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.FailPayment(payment)
            .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment))
            .Map(payment => new FailPaymentCommandResponse(payment is not null)))
            .ConfigureAwait(false);
    }
}
