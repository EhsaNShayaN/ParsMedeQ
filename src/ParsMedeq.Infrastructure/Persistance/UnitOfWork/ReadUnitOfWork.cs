using ParsMedeQ.Application.Persistance.Schema;
using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
using ParsMedeQ.Application.Persistance.Schema.LocationRepositories;
using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
using ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.UnitOfWork;

public sealed class ReadUnitOfWork : UnitofWorkBase<ReadDbContext>, IReadUnitOfWork
{
    public IUserReadRepository UserReadRepository => this.GetService<IUserReadRepository>();

    public IResourceReadRepository ResourceReadRepository => this.GetService<IResourceReadRepository>();

    public IPurchaseReadRepository PurchaseReadRepository => this.GetService<IPurchaseReadRepository>();

    public IProductReadRepository ProductReadRepository => this.GetService<IProductReadRepository>();

    public IMediaReadRepository MediaReadRepository => this.GetService<IMediaReadRepository>();

    public ICartReadRepository CartReadRepository => this.GetService<ICartReadRepository>();

    public ICommentReadRepository CommentReadRepository => this.GetService<ICommentReadRepository>();

    public IOrderReadRepository OrderReadRepository => this.GetService<IOrderReadRepository>();

    public IPaymentReadRepository PaymentReadRepository => this.GetService<IPaymentReadRepository>();

    public ITicketReadRepository TicketReadRepository => this.GetService<ITicketReadRepository>();

    public ITreatmentCenterReadRepository TreatmentCenterReadRepository => this.GetService<ITreatmentCenterReadRepository>();

    public ILocationReadRepository LocationReadRepository => this.GetService<ILocationReadRepository>();

    public ReadUnitOfWork(ReadDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider) { }
}
