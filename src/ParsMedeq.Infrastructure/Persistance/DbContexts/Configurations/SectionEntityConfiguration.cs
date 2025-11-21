using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using ParsMedeQ.Domain.Aggregates.SectionAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class SectionEntityConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable(TableNames.Section);

        builder
            .HasMany(x => x.SectionTranslations)
            .WithOne(x => x.Section)
            .HasForeignKey(a => a.SectionId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

sealed class SectionTranslationEntityConfiguration : IEntityTypeConfiguration<SectionTranslation>
{
    public void Configure(EntityTypeBuilder<SectionTranslation> builder)
    {
        builder.ToTable(TableNames.SectionTranslation);
        builder.HasKey(a => a.Id);
    }
}
