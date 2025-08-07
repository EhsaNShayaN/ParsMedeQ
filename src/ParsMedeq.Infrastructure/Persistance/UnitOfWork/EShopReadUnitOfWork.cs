using EShop.Application.Persistance.ESopSchema;
using EShop.Application.Persistance.ESopSchema.ProductRepositories;
using EShop.Application.Persistance.ESopSchema.UserRepositories;
using EShop.Infrastructure.Persistance.DbContexts;

namespace EShop.Infrastructure.Persistance.UnitOfWork;

public sealed class EShopReadUnitOfWork : UnitofWorkBase<EshopReadDbContext>, IEShopReadUnitOfWork
{
    public IUserReadRepository UserReadRepository => this.GetService<IUserReadRepository>();

    public IProductReadRepository ProductReadRepository => this.GetService<IProductReadRepository>();

    public EShopReadUnitOfWork(EshopReadDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider) { }
}
