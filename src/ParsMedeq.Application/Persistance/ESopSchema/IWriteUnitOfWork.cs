using ParsMedeQ.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeQ.Application.Persistance.ESopSchema.UserRepositories;

namespace ParsMedeQ.Application.Persistance.ESopSchema;
public interface IWriteUnitOfWork : IBaseWriteUnitOfWork
{
    IUserWriteRepository UserWriteRepository { get; }
    IProductWriteRepository ProductWriteRepository { get; }
}
