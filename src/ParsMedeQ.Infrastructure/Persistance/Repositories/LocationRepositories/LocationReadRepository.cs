using ParsMedeQ.Application.Features.LcationFeatures.LcationListFeature;
using ParsMedeQ.Application.Persistance.Schema.LocationRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.LocationRepositories;
internal sealed class LocationReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ILocationReadRepository
{
    public LocationReadRepository(ReadDbContext dbContext) : base(dbContext) { }

    public async ValueTask<PrimitiveResult<LocationListDbQueryResponse[]>> FilterLocations(
        string langCode,
        CancellationToken cancellationToken)
    {
        return await this.DbContext.Location
            .Include(x => x.LocationTranslations)
            .Select(s => new
            LocationListDbQueryResponse(
                s.Id,
                s.ParentId,
                s.LocationTranslations.SingleOrDefault(s => s.LanguageCode == langCode).Title ?? string.Empty)
            )
            .Run(q => q.ToArrayAsync(cancellationToken), PrimitiveResult.Success(Array.Empty<LocationListDbQueryResponse>()));
    }
}

