using EShop.Application.Persistance.ESopSchema.ProductRepositories;
using EShop.Application.Persistance.ESopSchema.UserRepositories;

namespace EShop.Application.Persistance.ESopSchema;
public interface IEShopWriteUnitOfWork : IWriteUnitOfWork
{
    IUserWriteRepository UserWriteRepository { get; }
    IProductWriteRepository ProductWriteRepository { get; }
}
