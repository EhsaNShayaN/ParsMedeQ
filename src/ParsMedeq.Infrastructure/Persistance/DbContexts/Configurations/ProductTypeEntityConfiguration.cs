using EShop.Infrastructure.Persistance.DbContexts.Extensions;

namespace EShop.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductTypeEntityConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable(TableNames.ProductType);

        builder.Property(x => x.Slug).IsSlugColumn();
        builder.Property(x => x.Title).IsTitleColumn();

        builder
            .HasMany(a => a.Brands)
            .WithOne(a => a.ProductType)
            .HasForeignKey(a => a.ProductTypeId);

        builder.HasIndex(a => a.Slug);
    }
}


