using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.OrderAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Order);

        builder
            .HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(a => a.OrderId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(a => a.Id)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(o => o.FinalAmount)
            .ValueGeneratedOnAddOrUpdate()
            .HasComputedColumnSql("[TotalAmount] - [DiscountAmount]");
    }
}

sealed class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(TableNames.OrderItems);
        builder.HasKey(a => a.Id);

        builder
            .Property(o => o.Subtotal)
            .ValueGeneratedOnAddOrUpdate()
            .HasComputedColumnSql("[Quantity] * [UnitPrice]");
    }
}
