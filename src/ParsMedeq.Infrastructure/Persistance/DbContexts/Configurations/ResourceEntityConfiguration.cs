using ParsMedeq.Infrastructure.Persistance.DbContexts.Extensions;

namespace ParsMedeq.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ResourceEntityConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable(TableNames.Resource);

        builder.Property(x => x.Title).IsTitleColumn();
    }
}
