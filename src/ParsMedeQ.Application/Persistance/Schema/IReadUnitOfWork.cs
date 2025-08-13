using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.Schema;

public interface IReadUnitOfWork : IUnitOfWork
{
    IUserReadRepository UserReadRepository { get; }
    IProductReadRepository ProductReadRepository { get; }
}
