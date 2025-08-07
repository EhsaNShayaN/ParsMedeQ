namespace EShop.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductVariationEntityConfiguration : IEntityTypeConfiguration<ProductVariation>
{
    public void Configure(EntityTypeBuilder<ProductVariation> builder)
    {
        builder.ToTable(TableNames.ProductVariation);

        builder.Property(x => x.Value).IsUnicode(true).HasMaxLength(255);
        builder.Property(x => x.InternalValue).IsUnicode(true).HasMaxLength(255);

        builder.ComplexProperty(
            a => a.Price,
            cBuilder =>
            {
                cBuilder.Property(a => a.Value)
                    .HasColumnName("Price")
                    .HasColumnType("decimal(22,4)");

                cBuilder.Property(a => a.Currency)
                    .HasColumnName("Currency")
                    .HasColumnType("Varchar(3)");
            });
    }
}