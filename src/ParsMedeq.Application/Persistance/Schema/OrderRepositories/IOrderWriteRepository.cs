using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
public interface IOrderWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Order>> AddOrder(Order order);
    ValueTask<PrimitiveResult<Order>> FindByDependencies(int id, CancellationToken cancellationToken);
}
