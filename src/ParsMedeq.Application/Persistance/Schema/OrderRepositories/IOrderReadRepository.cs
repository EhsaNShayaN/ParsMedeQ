using ParsMedeQ.Application.Features.OrderFeatures.OrderListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
public interface IOrderReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Order>> FindByIdWithOrder(int id, CancellationToken cancellationToken);
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<OrderListDbQueryResponse>>> FilterOrders(
        BasePaginatedQuery paginated,
        int userId,
        int lastId,
        CancellationToken cancellationToken);
}
