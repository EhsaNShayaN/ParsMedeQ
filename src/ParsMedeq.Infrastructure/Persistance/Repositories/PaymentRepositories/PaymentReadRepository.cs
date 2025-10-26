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
            FullName = res.User.FullName.GetValue(),
            Title = res.Title,
            Description = res.Description,
            Status = res.Status,
            MediaPath = res.MediaPath,
            Code = res.Code,
            CreationDate = res.CreationDate,
            Answers = res.PaymentAnswers.Select(answer => new PaymentAnswerDbQueryResponse
            {
                Answer = answer.Answer,
                CreationDate = answer.CreationDate,
                FullName = answer.Users.FullName.GetValue(),
                MediaPath = answer.MediaPath,
            }).ToArray(),
        };

        var result = await this.DbContext.PaginateByPrimaryKey(
            this.DbContext.Payment
            //.Include(x => x.PaymentAnswers)
            .Include(x => x.Order),
            lastId,
             x => userId <= 0 || x.UserId == userId,
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

