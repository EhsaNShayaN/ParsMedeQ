using ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Extensions;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.PaymentRepositories;
internal sealed class PaymentWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IPaymentWriteRepository
{
    public PaymentWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Payment>> FindById(int id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<Payment, int>(id, cancellationToken);

    public ValueTask<PrimitiveResult<Payment>> FindByIdWithOrder(int id, CancellationToken cancellationToken) =>
        this.DbContext
            .Payment
            .Include(s => s.Order)
            .Where(s => s.Id.Equals(id))
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "سفارشی با شناسه مورد نظر پیدا نشد"));
    public ValueTask<PrimitiveResult<Payment>> AddPayment(Payment Payment) => this.Add(Payment);
    public ValueTask<PrimitiveResult<Payment>> ConfirmPayment(Payment payment) => this.Update(payment);
    public ValueTask<PrimitiveResult<Payment>> FailPayment(Payment payment) => this.Update(payment);
}
