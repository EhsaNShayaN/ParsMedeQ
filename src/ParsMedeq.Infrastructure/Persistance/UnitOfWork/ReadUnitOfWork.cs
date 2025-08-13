using ParsMedeQ.Application.Persistance.Schema;
using ParsMedeQ.Application.Persistance.Schema.ProductRepositories;
using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.UnitOfWork;

public sealed class ReadUnitOfWork : UnitofWorkBase<ReadDbContext>, IReadUnitOfWork
{
    public IUserReadRepository UserReadRepository => this.GetService<IUserReadRepository>();

    public IProductReadRepository ProductReadRepository => this.GetService<IProductReadRepository>();

    public IResourceReadRepository ResourceReadRepository => this.GetService<IResourceReadRepository>();

    public IPurchaseReadRepository PurchaseReadRepository => this.GetService<IPurchaseReadRepository>();

    public ReadUnitOfWork(ReadDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider) { }
}
