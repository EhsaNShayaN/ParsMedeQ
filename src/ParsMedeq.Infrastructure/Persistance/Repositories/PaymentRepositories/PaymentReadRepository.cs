using ParsMedeQ.Application.Features.PaymentFeatures.PaymentListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using ParsMedeQ.Infrastructure.Persistance.DbContexts.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.PaymentRepositories;
internal sealed class PaymentReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IPaymentReadRepository
{
    public PaymentReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public async ValueTask<PrimitiveResult<BasePaginatedApiResponse<PaymentListDbQueryResponse>>> FilterPayments(
        BasePaginatedQuery paginated,
        int userId,
        int lastId,
        CancellationToken cancellationToken)
    {
        Expression<Func<Payment, PaymentListDbQueryResponse>> PaymentKeySelector = (res) => new PaymentListDbQueryResponse
        {
            Id = res.Id,
            OrderId = res.OrderId,
            Amount = res.Amount,
            PaymentMethod = res.PaymentMethod,
            TransactionId = res.TransactionId,
            Status = res.Status,
            PaidDate = res.PaidDate,
            CreationDate = res.CreationDate,
        };

        var result = await this.DbContext.PaginateByPrimaryKey(
            this.DbContext.Payment.Include(x => x.Order)
            //.Include(x => x.PaymentAnswers)
            .Include(x => x.Order),
            lastId,
             x => userId <= 0 || x.Order.UserId == userId,
             paginated.PageSize,
             PaymentKeySelector,
             cancellationToken)
            .Map(data => new BasePaginatedApiResponse<PaymentListDbQueryResponse>(
                data.Data,
               Convert.ToInt32(data.TotalCount),
            paginated.PageIndex,
            paginated.PageSize))
            .ConfigureAwait(false);
        return result;
    }
}

