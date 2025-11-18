using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.OrderAggregate.Entities;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
public interface IOrderWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Order>> AddOrder(Order order);
    ValueTask<PrimitiveResult<Order>> FindByDependencies(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Order>> PayOrder(Order order);
    ValueTask<PrimitiveResult<PeriodicService>> FindPeriodicServiceById(int id, CancellationToken cancellationToken);
}
