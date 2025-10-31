using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using ParsMedeQ.Infrastructure.Persistance.DbContexts.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.TreatmentCenterRepositories;
internal sealed class TreatmentCenterReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ITreatmentCenterReadRepository
{
    public TreatmentCenterReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult<TreatmentCenter>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<TreatmentCenter, int>(id, cancellationToken);
    public async ValueTask<PrimitiveResult<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>> FilterTreatmentCenters(
        BasePaginatedQuery paginated,
        string langCode,
        int lastId,
        CancellationToken cancellationToken)
    {
        Expression<Func<TreatmentCenter, TreatmentCenterListDbQueryResponse>> TreatmentCenterKeySelector = (res) => new TreatmentCenterListDbQueryResponse(
            res.Id,
            res.ProvinceId,
            res.CityId,
            res.TreatmentCenterTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
            res.TreatmentCenterTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Description ?? string.Empty,
            res.TreatmentCenterTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,
            res.CreationDate);

        var result = await this.DbContext.PaginateByPrimaryKey(
            this.DbContext.TreatmentCenter
            .Include(x => x.TreatmentCenterTranslations),
            lastId,
             x => true,
             paginated.PageSize,
             TreatmentCenterKeySelector,
             cancellationToken)
            .Map(data => new BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>(
                data.Data,
               Convert.ToInt32(data.TotalCount),
            paginated.PageIndex,
            paginated.PageSize))
            .ConfigureAwait(false);
        return result;
    }
}