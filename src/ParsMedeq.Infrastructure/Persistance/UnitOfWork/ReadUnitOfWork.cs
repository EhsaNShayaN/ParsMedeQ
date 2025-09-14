using ParsMedeQ.Application.Persistance.Schema;
using ParsMedeQ.Application.Persistance.Schema.MediaRepositories;
using ParsMedeQ.Application.Persistance.Schema.PurchaseRepositories;
using ParsMedeQ.Application.Persistance.Schema.ResourceRepositories;
using ParsMedeQ.Application.Persistance.Schema.UserRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.UnitOfWork;

public sealed class ReadUnitOfWork : UnitofWorkBase<ReadDbContext>, IReadUnitOfWork
{
    public IUserReadRepository UserReadRepository => this.GetService<IUserReadRepository>();

    public IResourceReadRepository ResourceReadRepository => this.GetService<IResourceReadRepository>();

    public IPurchaseReadRepository PurchaseReadRepository => this.GetService<IPurchaseReadRepository>();

    public IMediaReadRepository MediaReadRepository => this.GetService<IMediaReadRepository>();

    public ReadUnitOfWork(ReadDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider) { }
}
