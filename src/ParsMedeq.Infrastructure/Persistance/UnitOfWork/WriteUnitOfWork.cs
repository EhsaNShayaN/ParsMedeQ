using ParsMedeQ.Application.Persistance.Schema;
using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
using ParsMedeQ.Application.Persistance.Schema.LocationRepositories;
using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
using ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.UnitOfWork;

public sealed class WriteUnitOfWork : WriteUnitofWorkBase<WriteDbContext>, IWriteUnitOfWork
{
    public IUserWriteRepository UserWriteRepository => this.GetService<IUserWriteRepository>();
    public IResourceWriteRepository ResourceWriteRepository => this.GetService<IResourceWriteRepository>();
    public IProductWriteRepository ProductWriteRepository => this.GetService<IProductWriteRepository>();
    public IMediaWriteRepository MediaWriteRepository => this.GetService<IMediaWriteRepository>();
    public ICartWriteRepository CartWriteRepository => this.GetService<ICartWriteRepository>();
    public ICommentWriteRepository CommentWriteRepository => this.GetService<ICommentWriteRepository>();
    public IOrderWriteRepository OrderWriteRepository => this.GetService<IOrderWriteRepository>();
    public IPaymentWriteRepository PaymentWriteRepository => this.GetService<IPaymentWriteRepository>();
    public ITicketWriteRepository TicketWriteRepository => this.GetService<ITicketWriteRepository>();
    public ITreatmentCenterWriteRepository TreatmentCenterWriteRepository => this.GetService<ITreatmentCenterWriteRepository>();
    public ILocationWriteRepository LocationWriteRepository => this.GetService<ILocationWriteRepository>();

    #region " Constructors "
    public WriteUnitOfWork(
        WriteDbContext dbContext,
        IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
    }
    #endregion
}