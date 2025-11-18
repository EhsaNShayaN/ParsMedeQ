using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.OrderAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate;
using ParsMedeQ.Domain.Aggregates.PaymentAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Order);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(a => a.Id)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(a => a.OrderId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Payment)
            .WithOne(x => x.Order)
            .HasPrincipalKey<Order>(x => x.Id)
            .HasForeignKey<Payment>(p => p.Id);

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

        builder
            .HasMany(x => x.PeriodicServices)
            .WithOne(x => x.OrderItem)
            .HasForeignKey(a => a.OrderItemId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
sealed class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(TableNames.Payment);
        builder.HasKey(a => a.Id);

        builder
            .HasMany(x => x.PaymentLogs)
            .WithOne(x => x.Payment)
            .HasForeignKey(a => a.PaymentId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

sealed class PaymentLogEntityConfiguration : IEntityTypeConfiguration<PaymentLog>
{
    public void Configure(EntityTypeBuilder<PaymentLog> builder)
    {
        builder.ToTable(TableNames.PaymentLog);
        builder.HasKey(a => a.Id);
    }
}

sealed class PeriodicServiceEntityConfiguration : IEntityTypeConfiguration<PeriodicService>
{
    public void Configure(EntityTypeBuilder<PeriodicService> builder)
    {
        builder.ToTable(TableNames.PeriodicService);

        builder.HasKey(a => a.Id);
    }
}
