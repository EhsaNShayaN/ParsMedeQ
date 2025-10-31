using ParsMedeQ.Domain.Aggregates.ProvinceAggregate;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ProvinceEntityConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable(TableNames.Province);

        builder
            .HasOne(pc => pc.Parent)
            .WithMany(pc => pc.Cities)
            .HasForeignKey(pc => pc.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}