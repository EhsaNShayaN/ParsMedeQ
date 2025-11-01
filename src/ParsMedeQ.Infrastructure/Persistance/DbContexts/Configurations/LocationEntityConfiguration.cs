using ParsMedeQ.Domain.Aggregates.LocationAggregate;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable(TableNames.Location);

        builder
            .HasOne(pc => pc.Parent)
            .WithMany(pc => pc.Children)
            .HasForeignKey(pc => pc.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}