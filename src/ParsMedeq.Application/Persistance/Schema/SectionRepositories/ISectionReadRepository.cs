using ParsMedeQ.Application.Features.SectionFeatures.SectionListFeature;
using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.SectionRepositories;
public interface ISectionReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<SectionListDbQueryResponse[]>> GetAll(string langCode, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Section>> FindById(int id, CancellationToken cancellationToken);
}
