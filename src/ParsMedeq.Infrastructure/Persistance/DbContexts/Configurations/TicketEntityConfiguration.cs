using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using ParsMedeQ.Domain.Aggregates.TicketAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable(TableNames.Ticket);

        builder
            .HasMany(x => x.TicketAnswers)
            .WithOne(x => x.Ticket)
            .HasForeignKey(a => a.TicketId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Tickets)
            .HasForeignKey(a => a.Id)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

sealed class TicketItemEntityConfiguration : IEntityTypeConfiguration<TicketAnswer>
{
    public void Configure(EntityTypeBuilder<TicketAnswer> builder)
    {
        builder.ToTable(TableNames.TicketAnswers);
        builder.HasKey(a => a.Id);

        builder
            .HasOne(x => x.Users)
            .WithMany(x => x.TicketAnswerss)
            .HasForeignKey(a => a.Id)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
