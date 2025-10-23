using ParsMedeQ.Application.Persistance.Schema.CartRepositories;
using ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Application.Persistance.Schema.OrderRepositories;
using ParsMedeQ.Application.Persistance.Schema.PaymentRepositories;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.Schema;
public interface IWriteUnitOfWork : IBaseWriteUnitOfWork
{
    IUserWriteRepository UserWriteRepository { get; }
    IResourceWriteRepository ResourceWriteRepository { get; }
    IProductWriteRepository ProductWriteRepository { get; }
    IMediaWriteRepository MediaWriteRepository { get; }
    ICartWriteRepository CartWriteRepository { get; }
    ICommentWriteRepository CommentWriteRepository { get; }
    IOrderWriteRepository OrderWriteRepository { get; }
    IPaymentWriteRepository PaymentWriteRepository { get; }
}
