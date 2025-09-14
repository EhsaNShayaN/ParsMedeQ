using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.Schema;

public interface IReadUnitOfWork : IUnitOfWork
{
    IUserReadRepository UserReadRepository { get; }
    IResourceReadRepository ResourceReadRepository { get; }
    IMediaReadRepository MediaReadRepository { get; }
    IPurchaseReadRepository PurchaseReadRepository { get; }
}
