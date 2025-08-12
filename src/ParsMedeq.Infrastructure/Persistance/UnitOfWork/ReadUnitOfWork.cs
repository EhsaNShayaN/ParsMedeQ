using ParsMedeq.Application.Persistance.ESopSchema;
using ParsMedeq.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeq.Application.Persistance.ESopSchema.UserRepositories;
using ParsMedeq.Infrastructure.Persistance.DbContexts;

namespace ParsMedeq.Infrastructure.Persistance.UnitOfWork;

public sealed class ReadUnitOfWork : UnitofWorkBase<ReadDbContext>, IReadUnitOfWork
{
    public IUserReadRepository UserReadRepository => this.GetService<IUserReadRepository>();

    public IProductReadRepository ProductReadRepository => this.GetService<IProductReadRepository>();

    public ReadUnitOfWork(ReadDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider) { }
}
