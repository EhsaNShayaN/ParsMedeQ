using ParsMedeQ.Domain.Aggregates.PaymentAggregate;

namespace ParsMedeQ.Application.Features.PaymentFeatures.AddPaymentFeature;
public sealed class AddPaymentCommandHandler : IPrimitiveResultCommandHandler<AddPaymentCommand, AddPaymentCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;

    public AddPaymentCommandHandler(IWriteUnitOfWork writeUnitOfWork)
    {
        this._writeUnitOfWork = writeUnitOfWork;
    }

    public async Task<PrimitiveResult<AddPaymentCommandResponse>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        return await
            _writeUnitOfWork.OrderWriteRepository.FindById(request.OrderId, cancellationToken)

            .MapIf(
                order => request.Amount.Equals(order.FinalAmount),
                order => PrimitiveResult.Failure<Payment>("", "Invalid payment amount."),
                order => Payment.Create(order.Id, order.FinalAmount!.Value, 0))
            .Map(payment => this._writeUnitOfWork.PaymentWriteRepository.AddPayment(payment)
            .Map(payment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => payment))
            .Map(payment => new AddPaymentCommandResponse(payment is not null)))
            .ConfigureAwait(false);
    }
}
