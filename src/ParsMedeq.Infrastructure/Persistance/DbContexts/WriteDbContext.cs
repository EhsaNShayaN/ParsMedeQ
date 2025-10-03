using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.PurchaseAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts;

public sealed class WriteDbContext : DbContextBase<WriteDbContext>
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ResourceCategory> ResourceCategory { get; set; }
    public DbSet<ResourceCategoryTranslation> ResourceCategoryTranslation { get; set; }
    public DbSet<ResourceCategoryRelations> ResourceCategoryRelations { get; set; }
    public DbSet<Resource> Resource { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductMedia> ProductMedia { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public WriteDbContext(DbContextOptions<WriteDbContext> opts) : base(opts) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);
        base.OnModelCreating(modelBuilder);
    }
}