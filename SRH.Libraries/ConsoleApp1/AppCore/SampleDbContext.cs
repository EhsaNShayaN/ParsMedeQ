using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.AppCore;
public sealed class SampleDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public SampleDbContext(DbContextOptions<SampleDbContext> opts) : base(opts) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("SampleUser");
        base.OnModelCreating(modelBuilder);
    }

}
public sealed class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
}

