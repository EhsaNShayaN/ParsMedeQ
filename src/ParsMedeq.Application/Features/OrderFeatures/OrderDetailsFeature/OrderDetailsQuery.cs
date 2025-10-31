using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.OrderFeatures.OrderDetailsFeature;
public sealed record OrderDetailsQuery(int Id) : IPrimitiveResultQuery<Order>;

sealed class OrderDetailsQueryHandler : IPrimitiveResultQueryHandler<OrderDetailsQuery, Order>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public OrderDetailsQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<Order>> Handle(OrderDetailsQuery request, CancellationToken cancellationToken)
    {
        return await this._readUnitOfWork.OrderReadRepository.FindByDetails(
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
    }
}