using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.TicketFeatures.TicketDetailsFeature;
public sealed record TicketDetailsQuery(int Id) : IPrimitiveResultQuery<Ticket>;

sealed class TicketDetailsQueryHandler : IPrimitiveResultQueryHandler<TicketDetailsQuery, Ticket>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public TicketDetailsQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<Ticket>> Handle(TicketDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.TicketReadRepository.FindById(
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}