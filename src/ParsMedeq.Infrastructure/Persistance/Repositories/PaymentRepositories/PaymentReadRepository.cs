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
        Expression<Func<Payment, PaymentListDbQueryResponse>> PaymentKeySelector = (res) => new PaymentListDbQueryResponse(
            res.Id,
            res.Amount,
            res.PaymentMethod,
            res.TransactionId,
            res.Status,
            res.Order.User.FullName.GetValue(),
            res.PaidDate,
            res.CreationDate);

        var result = await this.DbContext.PaginateByPrimaryKey(
            this.DbContext.Payment
            .Include(x => x.Order)
            .ThenInclude(s => s.User),
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

