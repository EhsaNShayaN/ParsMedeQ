namespace EShop.Infrastructure.Persistance.UnitOfWork;

public abstract class UnitofWorkBase<TDbContext>
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;
    protected readonly IServiceProvider _serviceProvider;

    public UnitofWorkBase(TDbContext dbContext, IServiceProvider serviceProvider)
    {
        this._dbContext = dbContext;
        this._serviceProvider = serviceProvider;
    }

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
    protected T GetService<T>() => _serviceProvider.GetRequiredService<T>()!;
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
}
