using ParsMedeQ.Domain.Aggregates.ServiceAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ServiceRepositories;
public interface IServiceWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Service>> AddService(Service service);
}
