using EShop.Infrastructure.Persistance.DbContexts.Extensions;

namespace EShop.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Product);

        builder.Property(x => x.Slug).IsSlugColumn();
        builder.Property(x => x.Title).IsTitleColumn();

        builder
            .HasOne(a => a.ProductType)
            .WithMany()
            .HasForeignKey(a => a.ProductTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(a => a.Model)
            .WithMany(a => a.Products)
            .HasForeignKey(a => a.ModelId);

        builder
           .HasMany(a => a.Variations)
           .WithOne(a => a.Product)
           .HasForeignKey(a => a.ProductId);

        builder.HasIndex(a => a.Slug);
    }
}


