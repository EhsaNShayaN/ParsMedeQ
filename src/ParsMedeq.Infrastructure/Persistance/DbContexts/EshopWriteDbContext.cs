namespace EShop.Infrastructure.Persistance.DbContexts;

public sealed class EshopWriteDbContext : DbContextBase<EshopWriteDbContext>
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProductType> ProductType { get; set; }
    public DbSet<ProductBrand> ProductBrand { get; set; }
    public DbSet<ProductModel> ProductModel { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }

    public EshopWriteDbContext(DbContextOptions<EshopWriteDbContext> opts) : base(opts) { }
}