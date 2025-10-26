using ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.TicketRepositories;
internal sealed class TicketWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ITicketWriteRepository
{
    public TicketWriteRepository(WriteDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<Ticket>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Ticket, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<Ticket>> FindByIdWithAnswers(int id, CancellationToken cancellationToken) =>
        this.DbContext
            .Ticket
            .Include(s => s.TicketAnswers)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "تیکتی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult<Ticket>> AddTicket(Ticket Ticket) => this.Add(Ticket);
    public ValueTask<PrimitiveResult<Ticket>> DeleteTicket(Ticket Ticket) => this.Remove(Ticket);
}
