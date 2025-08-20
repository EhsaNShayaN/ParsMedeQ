using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ResourceCategoryEntityConfiguration : IEntityTypeConfiguration<ResourceCategory>
{
    public void Configure(EntityTypeBuilder<ResourceCategory> builder)
    {
        builder.ToTable(TableNames.ResourceCategory);

        //builder.Property(x => x.Title).IsTitleColumn();

        builder
            .HasOne(pc => pc.Parent)
            .WithMany(pc => pc.Children)
            .HasForeignKey(pc => pc.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.ResourceCategoryTranslations)
            .WithOne(x => x.ResourceCategory)
            .HasForeignKey(a => a.ResourceCategoryId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

sealed class ResourceCategoryTranslationEntityConfiguration : IEntityTypeConfiguration<ResourceCategoryTranslation>
{
    public void Configure(EntityTypeBuilder<ResourceCategoryTranslation> builder)
    {
        builder.ToTable(TableNames.ResourceCategoryTranslation);

        builder.HasKey(a => a.Id);


    }
}