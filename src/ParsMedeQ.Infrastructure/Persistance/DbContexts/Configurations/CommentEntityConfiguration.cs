using ParsMedeQ.Domain.Aggregates.CommentAggregate;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(TableNames.Comment);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(a => a.UserId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}