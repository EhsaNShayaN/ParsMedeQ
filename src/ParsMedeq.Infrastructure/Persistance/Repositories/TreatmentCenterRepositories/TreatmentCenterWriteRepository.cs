using ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.TreatmentCenterRepositories;
internal sealed class TreatmentCenterWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ITreatmentCenterWriteRepository
{
    public TreatmentCenterWriteRepository(WriteDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<TreatmentCenter>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<TreatmentCenter, int>(id, cancellationToken);
    public ValueTask<PrimitiveResult<TreatmentCenter>> AddTreatmentCenter(TreatmentCenter TreatmentCenter) => this.Add(TreatmentCenter);
    public ValueTask<PrimitiveResult<TreatmentCenter>> DeleteTreatmentCenter(TreatmentCenter TreatmentCenter) => this.Remove(TreatmentCenter);
}
