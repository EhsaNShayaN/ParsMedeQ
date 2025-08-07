using EShop.Application.Persistance.ESopSchema.ProductRepositories;
using EShop.Application.Persistance.ESopSchema.UserRepositories;

namespace EShop.Application.Persistance.ESopSchema;

public interface IEShopReadUnitOfWork : IUnitOfWork
{
    IUserReadRepository UserReadRepository { get; }
    IProductReadRepository ProductReadRepository { get; }
}
