using ParsMedeq.Domain.Aggregates.ResourceAggregate.Entities;

namespace EShop.Infrastructure.Persistance.DbContexts;
public sealed class EshopReadDbContext : DbContextBase<EshopReadDbContext>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ResourceCategory> ResourceCategory { get; set; }
    public DbSet<ResourceCategoryRelations> ResourceCategoryRelations { get; set; }
    public DbSet<Resource> Resource { get; set; }

    public EshopReadDbContext(DbContextOptions<EshopReadDbContext> opts) : base(opts) { }
}