using ParsMedeQ.Application.Persistance.ESopSchema;
using ParsMedeQ.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeQ.Application.Persistance.ESopSchema.UserRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.UnitOfWork;

public sealed class ReadUnitOfWork : UnitofWorkBase<ReadDbContext>, IReadUnitOfWork
{
    public IUserReadRepository UserReadRepository => this.GetService<IUserReadRepository>();

    public IProductReadRepository ProductReadRepository => this.GetService<IProductReadRepository>();

    public ReadUnitOfWork(ReadDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider) { }
}
