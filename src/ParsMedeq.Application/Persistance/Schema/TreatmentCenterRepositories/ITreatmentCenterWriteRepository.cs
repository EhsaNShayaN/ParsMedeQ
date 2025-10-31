using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
public interface ITreatmentCenterWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<TreatmentCenter>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<TreatmentCenter>> AddTreatmentCenter(TreatmentCenter TreatmentCenter);
    ValueTask<PrimitiveResult<TreatmentCenter>> DeleteTreatmentCenter(TreatmentCenter TreatmentCenter);
}
