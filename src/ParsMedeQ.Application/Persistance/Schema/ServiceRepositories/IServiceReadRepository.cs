using ParsMedeQ.Domain.Aggregates.ServiceAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ServiceRepositories;
public interface IServiceReadRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Service>> FindById(int id, CancellationToken cancellationToken);
}