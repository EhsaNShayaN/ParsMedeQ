using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
using ParsMedeQ.Application.Persistance.Schema.LocationRepositories;
using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.ServiceRepositories;
using ParsMedeQ.Application.Persistance.Schema.TicketRepositories;
using ParsMedeQ.Application.Persistance.Schema.TreatmentCenterRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.Schema;

public interface IReadUnitOfWork : IUnitOfWork
{
    IUserReadRepository UserReadRepository { get; }
    IResourceReadRepository ResourceReadRepository { get; }
    IProductReadRepository ProductReadRepository { get; }
    IMediaReadRepository MediaReadRepository { get; }
    IPurchaseReadRepository PurchaseReadRepository { get; }
    ICartReadRepository CartReadRepository { get; }
    ICommentReadRepository CommentReadRepository { get; }
    IOrderReadRepository OrderReadRepository { get; }
    IPaymentReadRepository PaymentReadRepository { get; }
    ITicketReadRepository TicketReadRepository { get; }
    ITreatmentCenterReadRepository TreatmentCenterReadRepository { get; }
    ILocationReadRepository LocationReadRepository { get; }
    IServiceReadRepository ServiceReadRepository { get; }
}
