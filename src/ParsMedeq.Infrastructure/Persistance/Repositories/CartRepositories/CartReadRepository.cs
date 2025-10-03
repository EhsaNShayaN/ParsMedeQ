using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CartRepositories;
internal sealed class CartReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, ICartReadRepository
{
    public CartReadRepository(ReadDbContext dbContext) : base(dbContext) { }
}

