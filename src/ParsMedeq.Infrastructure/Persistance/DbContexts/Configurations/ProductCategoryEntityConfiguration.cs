using EShop.Infrastructure.Persistance.DbContexts.Extensions;

namespace EShop.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductCategoryEntityConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable(TableNames.ProductCategory);

        builder.Property(x => x.Slug).IsSlugColumn();
        builder.Property(x => x.Title).IsTitleColumn();

        builder
            .HasOne(pc => pc.Parent)
            .WithMany(pc => pc.Children)
            .HasForeignKey(pc => pc.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(a => a.ProductType)
            .WithMany()
            .HasForeignKey(a => a.ProductTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}


