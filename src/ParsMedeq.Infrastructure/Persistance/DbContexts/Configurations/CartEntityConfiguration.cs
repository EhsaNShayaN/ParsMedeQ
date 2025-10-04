using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.CartAggregate.Entities;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable(TableNames.Cart);

        /*builder
            .HasMany(x => x.CartItems)
            .WithOne(x => x.Cart)
            .HasForeignKey(a => a.CartId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);*/
    }
}

sealed class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable(TableNames.CartItems);
        builder.HasKey(a => a.Id);
    }
}
