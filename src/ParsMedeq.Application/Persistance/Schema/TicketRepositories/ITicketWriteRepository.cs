using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
public interface ITicketWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Ticket>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Ticket>> FindByIdWithAnswers(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Ticket>> AddTicket(Ticket Ticket);
    ValueTask<PrimitiveResult<Ticket>> DeleteTicket(Ticket Ticket);
}
