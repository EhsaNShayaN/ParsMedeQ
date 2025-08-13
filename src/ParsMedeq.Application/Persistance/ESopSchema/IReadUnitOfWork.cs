using ParsMedeQ.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeQ.Application.Persistance.ESopSchema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.ESopSchema;

public interface IReadUnitOfWork : IUnitOfWork
{
    IUserReadRepository UserReadRepository { get; }
    IProductReadRepository ProductReadRepository { get; }
}
