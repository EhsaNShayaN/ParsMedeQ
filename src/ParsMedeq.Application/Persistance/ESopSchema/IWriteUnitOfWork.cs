using ParsMedeq.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeq.Application.Persistance.ESopSchema.UserRepositories;

namespace ParsMedeq.Application.Persistance.ESopSchema;
public interface IWriteUnitOfWork : IBaseWriteUnitOfWork
{
    IUserWriteRepository UserWriteRepository { get; }
    IProductWriteRepository ProductWriteRepository { get; }
}
