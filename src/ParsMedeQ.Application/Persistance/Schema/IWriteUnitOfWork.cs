using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.Schema;
public interface IWriteUnitOfWork : IBaseWriteUnitOfWork
{
    IUserWriteRepository UserWriteRepository { get; }
    IResourceWriteRepository ResourceWriteRepository { get; }
    IMediaWriteRepository MediaWriteRepository { get; }
    IProductWriteRepository ProductWriteRepository { get; }
}
