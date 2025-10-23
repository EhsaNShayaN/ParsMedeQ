using ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.PaymentRepositories;
internal sealed class PaymentReadRepository : GenericPrimitiveReadRepositoryBase<ReadDbContext>, IPaymentReadRepository
{
    public PaymentReadRepository(ReadDbContext dbContext) : base(dbContext) { }
}

