using ParsMedeQ.Domain.Aggregates.ServiceAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.ServiceRepositories;
public interface IServiceWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Service>> FindById(int id, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<Service>> AddService(Service service);
    ValueTask<PrimitiveResult<Service>> UpdateService(Service Service, CancellationToken cancellationToken);
}
