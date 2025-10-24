using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
public interface IOrderReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Order>> FindByIdWithPayment(int id, CancellationToken cancellationToken);
}
