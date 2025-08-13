using ParsMedeQ.Domain.Aggregates.ResourceCategoryAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ResourceCategoryRelationsEntityConfiguration : IEntityTypeConfiguration<ResourceCategoryRelations>
{
    public void Configure(EntityTypeBuilder<ResourceCategoryRelations> builder)
    {
        builder.ToTable(TableNames.ResourceCategoryRelations);

        builder
            .HasOne(pc => pc.Resource)
            .WithMany(pc => pc.ResourceCategoryRelations)
            .HasForeignKey(pc => pc.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(pc => pc.ResourceCategory)
            .WithMany(pc => pc.ResourceCategoryRelations)
            .HasForeignKey(pc => pc.ResourceCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
