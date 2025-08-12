using ParsMedeq.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeq.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeq.Infrastructure.Persistance.DbContexts;

public sealed class WriteDbContext : DbContextBase<WriteDbContext>
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ResourceCategory> ResourceCategory { get; set; }
    public DbSet<ResourceCategoryRelations> ResourceCategoryRelations { get; set; }
    public DbSet<Resource> Resource { get; set; }

    public WriteDbContext(DbContextOptions<WriteDbContext> opts) : base(opts) { }
}