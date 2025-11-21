using ParsMedeQ.Application.Persistance.Schema.SectionRepositories;
using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.SectionRepositories;
internal sealed class SectionWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ISectionWriteRepository
{
    public SectionWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Section>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Section, int>(id, cancellationToken);

    public ValueTask<PrimitiveResult<Section>> FindByTranslation(int id, CancellationToken cancellationToken)
    {
        var q = this.DbContext.Section.Include(s => s.SectionTranslations).Where(s => s.Id.Equals(id));
        return q.Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "آیتمی پیدا نشد"));
    }
    public ValueTask<PrimitiveResult<Section>> AddSection(Section Section) => this.Add(Section);
    public ValueTask<PrimitiveResult<Section>> EditSection(Section Section) => this.Update(Section);
    public ValueTask<PrimitiveResult<Section>> SetOrder(Section Section) => this.Update(Section);
}
