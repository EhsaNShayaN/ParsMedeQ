using ParsMedeQ.Domain.Aggregates.ProductAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Product);

        builder
            .HasMany(x => x.ProductTranslations)
            .WithOne(x => x.Product)
            .HasForeignKey(a => a.ProductId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ProductMediaList)
            .WithOne(x => x.Product)
            .HasForeignKey(a => a.ProductId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

sealed class ProductTranslationEntityConfiguration : IEntityTypeConfiguration<ProductTranslation>
{
    public void Configure(EntityTypeBuilder<ProductTranslation> builder)
    {
        builder.ToTable(TableNames.ProductTranslation);

        builder.HasKey(a => a.Id);
    }
}