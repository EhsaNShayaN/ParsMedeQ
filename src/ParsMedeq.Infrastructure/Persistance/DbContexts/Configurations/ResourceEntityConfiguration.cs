using ParsMedeQ.Domain.Aggregates.ResourceAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ResourceEntityConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable(TableNames.Resource);

        //builder.Property(x => x.Title).IsTitleColumn();

        builder
            .HasMany(x => x.ResourceTranslations)
            .WithOne(x => x.Resource)
            .HasForeignKey(a => a.ResourceId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

sealed class ResourceTranslationEntityConfiguration : IEntityTypeConfiguration<ResourceTranslation>
{
    public void Configure(EntityTypeBuilder<ResourceTranslation> builder)
    {
        builder.ToTable(TableNames.ResourceTranslation);
        builder.HasKey(a => a.Id);
    }
}
