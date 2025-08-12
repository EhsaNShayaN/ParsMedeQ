using ParsMedeq.Application.Persistance.ESopSchema;
using ParsMedeq.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeq.Application.Persistance.ESopSchema.UserRepositories;
using ParsMedeq.Infrastructure.Persistance.DbContexts;

namespace ParsMedeq.Infrastructure.Persistance.UnitOfWork;

public sealed class WriteUnitOfWork : WriteUnitofWorkBase<WriteDbContext>, IWriteUnitOfWork
{
    public IUserWriteRepository UserWriteRepository => this.GetService<IUserWriteRepository>();
    public IProductWriteRepository ProductWriteRepository => this.GetService<IProductWriteRepository>();

    #region " Constructors "
    public WriteUnitOfWork(
        WriteDbContext dbContext,
        IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
    }
    #endregion
}
