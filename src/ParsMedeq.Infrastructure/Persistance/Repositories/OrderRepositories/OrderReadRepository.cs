using ParsMedeQ.Application.Features.OrderFeatures.OrderListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using ParsMedeQ.Infrastructure.Persistance.DbContexts.Extensions;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.OrderRepositories;
internal sealed class OrderReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IOrderReadRepository
{
    public OrderReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Order>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Order, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<Order>> FindByIdWithOrder(int id, CancellationToken cancellationToken) =>
        this.DbContext
            .Order
            .Include(s => s.OrderItems)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "سفارشی با شناسه مورد نظر پیدا نشد"));
    public async ValueTask<PrimitiveResult<BasePaginatedApiResponse<OrderListDbQueryResponse>>> FilterOrders(
        BasePaginatedQuery paginated,
        int userId,
        int lastId,
        CancellationToken cancellationToken)
    {
        Expression<Func<Order, OrderListDbQueryResponse>> OrderKeySelector = (res) => new OrderListDbQueryResponse
        {
            Id = res.Id,
            UserId = res.UserId,
            OrderNumber = res.OrderNumber,
            TotalAmount = res.TotalAmount,
            DiscountAmount = res.DiscountAmount,
            FinalAmount = res.FinalAmount,
            Status = res.Status,
            UpdateDate = res.UpdateDate,
            CreationDate = res.CreationDate,
        };

        var result = await this.DbContext.PaginateByPrimaryKey(
            this.DbContext.Order.Include(x => x.Id)
            //.Include(x => x.OrderAnswers)
            .Include(x => x.OrderItems),
            lastId,
             x => userId <= 0 || x.UserId == userId,
             paginated.PageSize,
             OrderKeySelector,
             cancellationToken)
            .Map(data => new BasePaginatedApiResponse<OrderListDbQueryResponse>(
                data.Data,
               Convert.ToInt32(data.TotalCount),
            paginated.PageIndex,
            paginated.PageSize))
            .ConfigureAwait(false);
        return result;
    }
}

