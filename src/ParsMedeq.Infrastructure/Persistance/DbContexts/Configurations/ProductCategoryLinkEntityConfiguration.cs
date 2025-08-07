namespace EShop.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductCategoryLinkEntityConfiguration : IEntityTypeConfiguration<ProductCategoryLink>
{
    public void Configure(EntityTypeBuilder<ProductCategoryLink> builder)
    {
        builder.ToTable(TableNames.ProductCategoryLink);

        builder
            .HasOne(pcl => pcl.Product)
            .WithMany(p => p.CategoryLinks)
            .HasForeignKey(pcl => pcl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pcl => pcl.ProductCategory)
            .WithMany(pc => pc.ProductLinks)
            .HasForeignKey(pcl => pcl.ProductCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.ProductId);
    }
}



