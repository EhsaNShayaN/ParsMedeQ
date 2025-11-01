using ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.TreatmentCenterRepositories;
internal sealed class TreatmentCenterWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ITreatmentCenterWriteRepository
{
    public TreatmentCenterWriteRepository(WriteDbContext dbContext) : base(dbContext) { }
    public async ValueTask<PrimitiveResult<TreatmentCenter>> FindById(int id, CancellationToken cancellationToken)
    {
        return await this.DbContext.TreatmentCenter
               .Include(x => x.TreatmentCenterTranslations)
               .Where(s => s.Id.Equals(id))
               .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "سانتری با شناسه مورد نظر پیدا نشد"));
    }
    public ValueTask<PrimitiveResult<TreatmentCenter>> AddTreatmentCenter(TreatmentCenter TreatmentCenter) => this.Add(TreatmentCenter);
    public ValueTask<PrimitiveResult<TreatmentCenter>> DeleteTreatmentCenter(TreatmentCenter TreatmentCenter) => this.Remove(TreatmentCenter);
    public ValueTask<PrimitiveResult<TreatmentCenter>> UpdateTreatmentCenter(TreatmentCenter TreatmentCenter, CancellationToken cancellationToken) =>
        this.Update(TreatmentCenter);
}
