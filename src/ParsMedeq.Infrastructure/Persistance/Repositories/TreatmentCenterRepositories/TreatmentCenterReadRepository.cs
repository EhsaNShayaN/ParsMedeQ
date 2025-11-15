using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterDetailsFeature;
using ParsMedeQ.Application.Features.TreatmentCenterFeatures.TreatmentCenterListFeature;
using ParsMedeQ.Application.Helpers;
using ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using ParsMedeQ.Infrastructure.Persistance.DbContexts.Extensions;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.TreatmentCenterRepositories;
internal sealed class TreatmentCenterReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ITreatmentCenterReadRepository
{
    public TreatmentCenterReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public async ValueTask<PrimitiveResult<TreatmentCenterDetailsDbQueryResponse>> FindById(int id, string langCode, CancellationToken cancellationToken)
    {
        return await this.DbContext.TreatmentCenter
            .Include(x => x.TreatmentCenterTranslations)
            .Where(s => s.Id.Equals(id))
            .Select(s => new TreatmentCenterDetailsDbQueryResponse(
                s.Id,
                s.ProvinceId,
                s.CityId,
                s.TreatmentCenterTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty,
                s.TreatmentCenterTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Description ?? string.Empty,
                s.TreatmentCenterTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Image ?? string.Empty,
                s.CreationDate
            ))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "سانتری با شناسه مورد نظر پیدا نشد"));
    }
    public async ValueTask<PrimitiveResult<BasePaginatedApiResponse<TreatmentCenterListDbQueryResponse>>> FilterTreatmentCenters(
        BasePaginatedQuery paginated,
        string langCode,
        string query,
        int provinceId,
        int cityId,
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
             x =>
                //(string.IsNullOrWhiteSpace(query) || x.ProvinceId.Equals(provinceId)) &&
                (provinceId.Equals(0) || x.ProvinceId.Equals(provinceId)) &&
                (cityId.Equals(0) || x.CityId.Equals(cityId)),
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