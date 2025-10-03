using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ProductCategoryAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.PurchaseAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts;
public sealed class ReadDbContext : DbContextBase<ReadDbContext>
{
    public DbSet<User> Users { get; set; }
    public DbSet<ResourceCategory> ResourceCategory { get; set; }
    public DbSet<ResourceCategoryTranslation> ResourceCategoryTranslation { get; set; }
    public DbSet<ResourceCategoryRelations> ResourceCategoryRelations { get; set; }
    public DbSet<Resource> Resource { get; set; }
    public DbSet<ResourceTranslation> ResourceTranslation { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductMedia> ProductMedia { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductCategoryTranslation> ProductCategoryTranslation { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<Media> Media { get; set; }

    public ReadDbContext(DbContextOptions<ReadDbContext> opts) : base(opts) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);
        base.OnModelCreating(modelBuilder);
    }
}