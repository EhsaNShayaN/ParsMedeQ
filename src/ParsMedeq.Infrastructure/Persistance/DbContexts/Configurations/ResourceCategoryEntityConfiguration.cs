using ParsMedeq.Domain.Aggregates.ResourceCategoryAggregate;
using ParsMedeq.Infrastructure.Persistance.DbContexts.Extensions;

namespace ParsMedeq.Infrastructure.Persistance.DbContexts.Configurations;

sealed class ResourceCategoryEntityConfiguration : IEntityTypeConfiguration<ResourceCategory>
{
    public void Configure(EntityTypeBuilder<ResourceCategory> builder)
    {
        builder.ToTable(TableNames.ResourceCategory);

        builder.Property(x => x.Title).IsTitleColumn();

        builder
            .HasOne(pc => pc.Parent)
            .WithMany(pc => pc.Children)
            .HasForeignKey(pc => pc.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
