using ParsMedeQ.Infrastructure.Persistance.ValueConverters;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Configurations;

sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.User);

        builder.Property(b => b.Id).UseIdentityColumn();

        builder.ComplexProperty(
            a => a.FullName,
            a =>
            {
                a.Property(x => x.FirstName).HasColumnName("FirstName").HasConversion<FirstNameTypeValueConverter, FirstNameTypeValueComparer>();
                a.Property(x => x.LastName).HasColumnName("LastName").HasConversion<LastNameTypeValueConverter, LastNameTypeValueComparer>();
            });

        builder.ComplexProperty(
            a => a.Password,
            a =>
            {
                a.Property(x => x.Value).HasMaxLength(2500).IsUnicode(false).HasColumnName("Password");
                a.Property(x => x.Salt).HasMaxLength(2500).IsUnicode(false).HasColumnName("Salt");
            });
    }
}