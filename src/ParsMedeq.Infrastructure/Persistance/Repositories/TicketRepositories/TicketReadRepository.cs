using ParsMedeQ.Application.Features.TicketFeatures.TicketListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Models;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.TicketRepositories;
internal sealed class TicketReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ITicketReadRepository
{
    public TicketReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<Ticket>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Ticket, int>(id, cancellationToken);

    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<TicketListDbQueryResponse>>> FilterTickets(
        BasePaginatedQuery paginated,
        int lastId,
        CancellationToken cancellationToken)
    {
        static BasePaginatedApiResponse<TicketListDbQueryResponse> MapResult(
            PaginateListResult<TicketListDbQueryResponse> paginatedData,
            BasePaginatedQuery pageinated)
        {
            var data = paginatedData.Data.ToArray();
            return new BasePaginatedApiResponse<TicketListDbQueryResponse>(
                data,
                paginatedData.Total,
                pageinated.PageIndex,
                pageinated.PageSize)
            {
                LastId = data.Length > 0 ? data.Last().Id : 0
            };
        }

        var query =
            from res in this.DbContext.Ticket
            select new TicketListDbQueryResponse
            {
                FullName = res.Users.FullName.GetValue(),
                Title = res.Title,
                Description = res.Description,
                Status = res.Status,
                MediaPath = res.MediaPath,
                Code = res.Code,
                CreationDate = res.CreationDate,
                Answers = res.TicketAnswerss.Select(answer => new TicketAnswerDbQueryResponse
                {
                    Answer = answer.Answer,
                    CreationDate = answer.CreationDate,
                    FullName = answer.Users.FullName.GetValue(),
                    MediaPath = answer.MediaPath,
                }).ToArray(),
            };

        if (lastId.Equals(0))
            return query.Paginate(
                PaginateQuery.Create(paginated.PageIndex, paginated.PageSize),
                s => s.Id,
                PaginateOrder.DESC,
                cancellationToken)
                .Map(data => MapResult(data, paginated));

        return query.PaginateOverPK(
            paginated.PageSize,
            lastId,
            PaginateOrder.DESC,
            cancellationToken)
            .Map(data => MapResult(data, paginated));
    }
}

