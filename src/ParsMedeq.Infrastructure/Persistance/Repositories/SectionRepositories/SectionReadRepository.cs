using ParsMedeQ.Application.Features.SectionFeatures.SectionListFeature;
using ParsMedeQ.Application.Persistance.Schema.SectionRepositories;
using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.SectionRepositories;
internal sealed class SectionReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ISectionReadRepository
{
    public SectionReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<SectionListDbQueryResponse[]>> GetAll(string langCode, CancellationToken cancellationToken)
    {
        var q = from res in this.DbContext.Section
                .Include(s => s.SectionTranslations)
                select new SectionListDbQueryResponse
                {
                    Id = res.Id,
                    SectionId = res.Id,
                    Title = res.SectionTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                    Description = res.SectionTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Description ?? string.Empty,
                    Image = res.SectionTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,
                    Hidden = res.Hidden,
                };
        return q.Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "آیتمی پیدا نشد"));
    }

    public ValueTask<PrimitiveResult<SectionListDbQueryResponse[]>> GetAllTranslations(string langCode, CancellationToken cancellationToken)
    {
        var q = from res in this.DbContext.SectionTranslation
                .Include(s => s.Section)
                .Where(s => s.LanguageCode == langCode)
                select new SectionListDbQueryResponse
                {
                    Id = res.Id,
                    SectionId = res.SectionId,
                    Title = res.Title,
                    Description = res.Description,
                    Image = res.Image,
                    Hidden = res.Section.Hidden,
                };
        return q.Run(q => q.ToArrayAsync(cancellationToken), PrimitiveError.Create("", "آیتمی پیدا نشد"));
    }

    public ValueTask<PrimitiveResult<Section>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Section, int>(id, cancellationToken);
}
