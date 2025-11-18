using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.OrderAggregate.Entities;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.OrderRepositories;
internal sealed class OrderWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IOrderWriteRepository
{
    public OrderWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Order, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<Order>> AddOrder(Order order) => this.Add(order);
    public ValueTask<PrimitiveResult<Order>> FindByDependencies(int id, CancellationToken cancellationToken) =>
        this.DbContext.Order
        .Include(s => s.OrderItems)
        .ThenInclude(s => s.PeriodicServices)
        .Where(s => s.Id.Equals(id))
        .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "سفارشی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult<Order>> PayOrder(Order order) => this.Update(order);
    public ValueTask<PrimitiveResult<PeriodicService>> FindPeriodicServiceById(int id, CancellationToken cancellationToken) =>
        this.DbContext.PeriodicService
        .Where(s => s.Id.Equals(id))
        .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "سفارشی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult> AddPeriodicServices(IEnumerable<PeriodicService> periodicServices) => this.AddRange(periodicServices);
}
