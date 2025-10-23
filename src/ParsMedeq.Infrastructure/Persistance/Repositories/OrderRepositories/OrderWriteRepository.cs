using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.OrderRepositories;
internal sealed class OrderWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IOrderWriteRepository
{
    public OrderWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Order, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<Order>> AddOrder(Order order) => this.Add(order);
}
