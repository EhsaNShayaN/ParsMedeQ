using ParsMedeQ.Application.Persistance.ESopSchema;
using ParsMedeQ.Application.Persistance.ESopSchema.ProductRepositories;
using ParsMedeQ.Application.Persistance.ESopSchema.UserRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;

namespace ParsMedeQ.Infrastructure.Persistance.UnitOfWork;

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
