using ParsMedeQ.Application.Features.TicketFeatures.TicketListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
public interface ITicketReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Ticket>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Ticket>> FindByIdWithAnswers(int id, CancellationToken cancellationToken);
    public ValueTask<PrimitiveResult<BasePaginatedApiResponse<TicketListDbQueryResponse>>> FilterTickets(
        BasePaginatedQuery paginated,
        int? userId,
        int lastId,
        CancellationToken cancellationToken);
}
