using SRH.Persistance.Extensions;

namespace SRH.Persistance.Repositories.Read;
public abstract partial class GenericPrimitiveReadRepositoryBase<TDbContext>
    where TDbContext : DbContext
{
    #region " Fields "
    private readonly TDbContext _dbContext;
    #endregion

    #region " Properties "
    protected TDbContext DbContext => this._dbContext;
    #endregion

    #region " Constructors "
    public GenericPrimitiveReadRepositoryBase(TDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    #endregion

    #region " FindByIdAsync "
    public virtual ValueTask<PrimitiveResult<TEntity>> FindByIdAsync<TEntity, TId>(TId id, CancellationToken cancellationToken) where TEntity : class =>
        PrimitiveMaybe.From(id)
            .MapValue(async id =>
            {
                var dbResult = await this._dbContext.Set<TEntity>()
                    .FindAsync(id, cancellationToken)
                    .ConfigureAwait(false);

                return dbResult.ToOption()
                    .MapValue(entity => PrimitiveResult.Success(entity))
                    .GetOr(PrimitiveResult.Failure<TEntity>(GenericPrimitiveReadRepositoryErrors.Entity_With_Id_Not_Found_Error));
            }).GetOr(PrimitiveResult.Failure<TEntity>(GenericPrimitiveReadRepositoryErrors.Given_Id_Is_Null_Error));
    #endregion

    #region " FirstOrDefaultByIdAsync "
    public virtual ValueTask<PrimitiveResult<TEntity>> FirstOrDefaultByIdAsync<TEntity, TId>(TId id, CancellationToken cancellationToken) where TEntity : class =>
        PrimitiveMaybe.From(id)
            .MapValue(id => this.GetPrimaryKeys<TEntity>()
                    .Ensure(primaryKeys => primaryKeys.Length == 1, GenericPrimitiveReadRepositoryErrors.Generate_Entity_HasComplexPrimaryKey_Error<TEntity>())
                    .Map(primaryKeys => this.GenerateEntityPrimaryKeyEqualityExpression<TEntity>(primaryKeys, id!))
                    .Map(async predicate => await this._dbContext.Set<TEntity>()
                            .Run(q => q.FirstOrDefaultAsync(predicate, cancellationToken),
                                GenericPrimitiveReadRepositoryErrors.Entity_With_Id_Not_Found_Error)
                            .ConfigureAwait(false)
                    ))
            .GetOr(PrimitiveResult.Failure<TEntity>(GenericPrimitiveReadRepositoryErrors.Given_Id_Is_Null_Error));
    #endregion

    #region " FirstOrDefaultAsync "
    public virtual ValueTask<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        PrimitiveError nullError,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return this.GenerateFilter(predicate, sort, projection)
            .Map(query => query.Run(q => q.FirstOrDefaultAsync(), nullError))!;
    }
    public virtual ValueTask<PrimitiveResult<TResult>> FirstOrDefaultAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        TResult defaultValue,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return this.GenerateFilter(predicate, sort, projection)
            .Map(query => query.Run(q => q.FirstOrDefaultAsync(), defaultValue!))!;
    }
    public async virtual ValueTask<PrimitiveResult<TEntity>> FirstOrDefaultAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        PrimitiveError nullError,
        CancellationToken cancellationToken) where TEntity : class
    {
        return await this._dbContext.Set<TEntity>()
            .Run(q =>
                q.FirstOrDefaultAsync(predicate, cancellationToken),
                nullError)
            .ConfigureAwait(false);
    }
    public virtual ValueTask<PrimitiveResult<TEntity>> FirstOrDefaultAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) where TEntity : class =>
            this.FirstOrDefaultAsync(predicate,
                GenericPrimitiveReadRepositoryErrors.Entity_With_Id_Not_Found_Error,
                cancellationToken);
    #endregion

    #region " ToListAsync "
    public virtual ValueTask<PrimitiveResult<List<TResult>>> ToListAsync<TEntity, TResult>(
       Expression<Func<TEntity, bool>> predicate,
       Expression<Func<TEntity, object>>? sort,
       Expression<Func<TEntity, TResult>>? projection,
       CancellationToken cancellationToken)
       where TEntity : class
    {
        return this.GenerateFilter(predicate, sort, projection)
            .Map(query => query.Run(q => q.ToListAsync(), Array.Empty<TResult>().ToList()))!;
    }

    public async virtual ValueTask<PrimitiveResult<List<TEntity>>> ToListAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) where TEntity : class
    {
        return await this._dbContext.Set<TEntity>()
            .Run(q => q.Where(predicate).ToListAsync(cancellationToken), PrimitiveResult.Success(Enumerable.Empty<TEntity>().ToList()))
            .ConfigureAwait(false);
    }
    #endregion

    #region " ToArrayAsync "
    public virtual ValueTask<PrimitiveResult<TResult[]>> ToArrayAsync<TEntity, TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        return this.GenerateFilter(predicate, sort, projection)
            .Map(query => query.Run(q => q.ToArrayAsync(), Array.Empty<TResult>()))!;
    }

    public async virtual ValueTask<PrimitiveResult<TEntity[]>> ToArrayAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) where TEntity : class
    {
        return await this._dbContext.Set<TEntity>()
            .Run(q => q.Where(predicate).ToArrayAsync(cancellationToken),
                PrimitiveResult.Success(Array.Empty<TEntity>()))
            .ConfigureAwait(false);
    }
    #endregion



    #region " Private Methods "
    private ValueTask<PrimitiveResult<string[]>> GetPrimaryKeys<TEntity>() where TEntity : class =>
       PrimitiveResult.Empty
           .Map(_ => this._dbContext.GetPrimaryKeys<TEntity>())
           .Bind(pks => pks.Any()
               ? PrimitiveResult.Success(pks)
               : PrimitiveResult.Failure<string[]>(GenericPrimitiveReadRepositoryErrors.Generate_Entity_PrimaryKey_Not_Found_Error<string[]>()));

    private ValueTask<PrimitiveResult<Expression<Func<TEntity, bool>>>> GenerateEntityPrimaryKeyEqualityExpression<TEntity>(string[] primaryKeys, object id) where TEntity : class =>
        PrimitiveResult.Success(primaryKeys)
            .Map(primaryKeys =>
            {
                // Create parameter expression
                ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
                // Create equality expressions for primary key property
                var equalityExpression = Expression.Equal(Expression.Property(parameter, primaryKeys.First()), Expression.Constant(id));
                // Create lambda expression
                Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equalityExpression, parameter);
                return PrimitiveResult.Success(lambda);
            });
    #endregion

    static IQueryable<T> OrderByColumns<T>(IQueryable<T> source, string[] columns)
    {
        if (columns == null || columns.Length == 0)
            return source;

        var type = typeof(T);
        var parameter = Expression.Parameter(type, "x");

        IQueryable<T> result = source;
        bool firstOrder = true;

        foreach (var column in columns)
        {
            var property = type.GetProperty(column);
            if (property == null)
                throw new ArgumentException($"Property '{column}' does not exist on type '{type.Name}'");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            string methodName = firstOrder ? "OrderBy" : "ThenBy";
            var orderByCall = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { type, property.PropertyType },
                result.Expression,
                Expression.Quote(orderByExp));

            result = result.Provider.CreateQuery<T>(orderByCall);
            firstOrder = false;
        }

        return result;
    }
    ValueTask<PrimitiveResult<IQueryable<TResult>>> GenerateFilter<TEntity, TResult>(
        Expression<Func<TEntity, bool>>? predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection)
        where TEntity : class
    {
        if (predicate is null)
        {
            predicate = (_) => true;
        }

        var q = this._dbContext.Set<TEntity>()
                        .Where(predicate);

        return (sort, projection) switch
        {
            (not null, not null) => ValueTask.FromResult(PrimitiveResult.Success(
                        q.OrderBy(sort!)
                        .Select(projection))),

            (not null, null) => ValueTask.FromResult(PrimitiveResult.Success(
                        q.OrderBy(sort!)
                        .Cast<TResult>())),

            (null, not null) => this.GetPrimaryKeys<TEntity>()
                .Map(primaryKeys => OrderByColumns(q, primaryKeys))
                .Map(a => a.Select(projection)),

            (null, null) => this.GetPrimaryKeys<TEntity>()
                .Map(primaryKeys => OrderByColumns(q, primaryKeys))
                .Map(a => a.Cast<TResult>())
        };
    }

}

