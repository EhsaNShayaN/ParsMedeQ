using EShop.Infrastructure.Persistance.DbContexts.Extensions;

namespace EShop.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductBrandEntityConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.ToTable(TableNames.ProductBrand);

        builder.Property(x => x.Slug).IsSlugColumn();
        builder.Property(x => x.Title).IsTitleColumn();

        builder
           .HasMany(a => a.Models)
           .WithOne(a => a.Brand)
           .HasForeignKey(a => a.ProductBrandId);

        builder.HasIndex(a => a.Slug);
    }
}
