using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.OrderRepositories;
internal sealed class OrderReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IOrderReadRepository
{
    public OrderReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Order, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<Order>> FindByIdWithPayment(int id, CancellationToken cancellationToken) =>
        this.DbContext
            .Order
            .Include(s => s.Payments)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "سفارشی با شناسه مورد نظر پیدا نشد"));
}

