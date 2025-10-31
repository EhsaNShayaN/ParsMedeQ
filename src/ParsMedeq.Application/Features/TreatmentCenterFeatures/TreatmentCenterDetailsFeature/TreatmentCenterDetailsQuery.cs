using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterDetailsFeature;
public sealed record TreatmentCenterDetailsQuery(int Id) : IPrimitiveResultQuery<TreatmentCenter>;

sealed class TreatmentCenterDetailsQueryHandler : IPrimitiveResultQueryHandler<TreatmentCenterDetailsQuery, TreatmentCenter>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public TreatmentCenterDetailsQueryHandler(
        IReadUnitOfWork taxMemoryReadUnitOfWork)
    {
        this._readUnitOfWork = taxMemoryReadUnitOfWork;
    }
    public async Task<PrimitiveResult<TreatmentCenter>> Handle(TreatmentCenterDetailsQuery request, CancellationToken cancellationToken) =>
        await this._readUnitOfWork.TreatmentCenterReadRepository.FindById(
            request.Id,
            cancellationToken)
        .ConfigureAwait(false);
}