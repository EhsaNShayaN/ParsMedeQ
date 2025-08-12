using ParsMedeq.Infrastructure.Persistance.DbContexts.Extensions;

namespace ParsMedeq.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Product);

        builder.Property(x => x.Title).IsTitleColumn();

    }
}
