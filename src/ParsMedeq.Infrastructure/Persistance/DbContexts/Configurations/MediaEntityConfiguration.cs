using ParsMedeQ.Domain.Aggregates.MediaAggregate;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class MediaEntityConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable(TableNames.Media);
    }
}