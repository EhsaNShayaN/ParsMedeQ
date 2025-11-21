using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.SectionRepositories;
public interface ISectionWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Section>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Section>> FindByTranslation(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Section>> AddSection(Section Section);
    ValueTask<PrimitiveResult<Section>> EditSection(Section Section);
    ValueTask<PrimitiveResult<Section>> SetOrder(Section Section);
}
