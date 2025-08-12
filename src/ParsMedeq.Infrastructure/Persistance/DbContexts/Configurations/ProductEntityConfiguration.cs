using EShop.Infrastructure.Persistance.DbContexts.Extensions;

namespace EShop.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Product);

        builder.Property(x => x.Slug).IsSlugColumn();
        builder.Property(x => x.Title).IsTitleColumn();

        builder.HasIndex(a => a.Slug);
    }
}
