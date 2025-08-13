using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.Schema;

public interface IReadUnitOfWork : IUnitOfWork
{
    IUserReadRepository UserReadRepository { get; }
    IResourceReadRepository ResourceReadRepository { get; }
    IPurchaseReadRepository PurchaseReadRepository { get; }
    IProductReadRepository ProductReadRepository { get; }
}
