using ParsMedeQ.Application.Features.TicketFeatures.TicketListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using ParsMedeQ.Infrastructure.Persistance.DbContexts.Extensions;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.TicketRepositories;
internal sealed class TicketReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ITicketReadRepository
{
    public TicketReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<Ticket>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Ticket, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<Ticket>> FindByIdWithAnswers(int id, CancellationToken cancellationToken) =>
        this.DbContext
            .Ticket
            .Include(s => s.TicketAnswers)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "تیکتی با شناسه مورد نظر پیدا نشد"));
    public async ValueTask<PrimitiveResult<BasePaginatedApiResponse<TicketListDbQueryResponse>>> FilterTickets(
        BasePaginatedQuery paginated,
        int userId,
        int lastId,
        CancellationToken cancellationToken)
    {
        Expression<Func<Ticket, TicketListDbQueryResponse>> TicketKeySelector = (res) => new TicketListDbQueryResponse
        {
            FullName = res.User.FullName.GetValue(),
            Title = res.Title,
            Description = res.Description,
            Status = res.Status,
            MediaPath = res.MediaPath,
            Code = res.Code,
            CreationDate = res.CreationDate,
            Answers = res.TicketAnswers.Select(answer => new TicketAnswerDbQueryResponse
            {
                Answer = answer.Answer,
                CreationDate = answer.CreationDate,
                FullName = answer.Users.FullName.GetValue(),
                MediaPath = answer.MediaPath,
            }).ToArray(),
        };

        return await this.DbContext.PaginateByPrimaryKey(
            this.DbContext.Ticket
            .Include(x => x.TicketAnswers)
            .Include(x => x.User),
            lastId,
             x => userId <= 0 || x.UserId == userId,
             paginated.PageSize,
             TicketKeySelector,
             cancellationToken)
            .Map(data => new BasePaginatedApiResponse<TicketListDbQueryResponse>(
                data.Data,
               Convert.ToInt32(data.TotalCount),
            paginated.PageIndex,
            paginated.PageSize))
            .ConfigureAwait(false);
    }
}