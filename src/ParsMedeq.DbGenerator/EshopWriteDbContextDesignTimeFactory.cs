using EShop.Infrastructure.Persistance.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EShop.DbGenerator;
internal class EshopWriteDbContextDesignTimeFactory : IDesignTimeDbContextFactory<EshopWriteDbContext>
{
    public EshopWriteDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var cnnstring = configuration.GetSection("Database:Eshop").Get<SqlConnectionStringBuilder>()!;

        var optionsBuilder = new DbContextOptionsBuilder<EshopWriteDbContext>();
        optionsBuilder.UseSqlServer(cnnstring.ConnectionString);

        return new EshopWriteDbContext(optionsBuilder.Options);
    }
}
