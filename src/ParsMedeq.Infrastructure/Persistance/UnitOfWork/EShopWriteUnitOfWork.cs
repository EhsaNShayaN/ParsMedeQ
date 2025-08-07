using EShop.Application.Persistance.ESopSchema;
using EShop.Application.Persistance.ESopSchema.ProductRepositories;
using EShop.Application.Persistance.ESopSchema.UserRepositories;
using EShop.Infrastructure.Persistance.DbContexts;

namespace EShop.Infrastructure.Persistance.UnitOfWork;

public sealed class EShopWriteUnitOfWork : WriteUnitofWorkBase<EshopWriteDbContext>, IEShopWriteUnitOfWork
{
    public IUserWriteRepository UserWriteRepository => this.GetService<IUserWriteRepository>();
    public IProductWriteRepository ProductWriteRepository => this.GetService<IProductWriteRepository>();

    #region " Constructors "
    public EShopWriteUnitOfWork(
        EshopWriteDbContext dbContext,
        IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
    }
    #endregion
}
