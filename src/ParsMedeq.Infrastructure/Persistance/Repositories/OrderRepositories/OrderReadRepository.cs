using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.OrderRepositories;
internal sealed class OrderReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IOrderReadRepository
{
    public OrderReadRepository(ReadDbContext dbContext) : base(dbContext) { }
}

