using ParsMedeQ.Domain.Aggregates.PaymentAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
public interface IPaymentWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Payment>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Payment>> AddPayment(Payment payment);
    ValueTask<PrimitiveResult<Payment>> ConfirmPayment(Payment payment);
    ValueTask<PrimitiveResult<Payment>> FailPayment(Payment payment);
}
