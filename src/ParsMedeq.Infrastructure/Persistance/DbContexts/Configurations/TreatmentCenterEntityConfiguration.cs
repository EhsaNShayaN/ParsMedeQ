using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate;
using ParsMedeQ.Domain.Aggregates.TreatmentCenterAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class TreatmentCenterEntityConfiguration : IEntityTypeConfiguration<TreatmentCenter>
{
    public void Configure(EntityTypeBuilder<TreatmentCenter> builder)
    {
        builder.ToTable(TableNames.TreatmentCenter);

        builder
            .HasMany(x => x.TreatmentCenterTranslations)
            .WithOne(x => x.TreatmentCenter)
            .HasForeignKey(a => a.TreatmentCenterId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Province)
            .WithMany()
            .HasForeignKey(a => a.ProvinceId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.City)
            .WithMany(x => x.TreatmentCenters)
            .HasForeignKey(a => a.CityId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

sealed class TreatmentCenterTranslationEntityConfiguration : IEntityTypeConfiguration<TreatmentCenterTranslation>
{
    public void Configure(EntityTypeBuilder<TreatmentCenterTranslation> builder)
    {
        builder.ToTable(TableNames.TreatmentCenterTranslation);
        builder.HasKey(a => a.Id);
    }
}
