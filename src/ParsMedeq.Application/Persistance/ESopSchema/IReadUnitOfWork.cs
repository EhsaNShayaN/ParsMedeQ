using ParsMedeq.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeq.Application.Persistance.ESopSchema.UserRepositories;

namespace ParsMedeq.Application.Persistance.ESopSchema;

public interface IReadUnitOfWork : IUnitOfWork
{
    IUserReadRepository UserReadRepository { get; }
    IProductReadRepository ProductReadRepository { get; }
}
