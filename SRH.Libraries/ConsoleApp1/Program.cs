using ConsoleApp1.AppCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

CreateHostBuilder(null).Build().Run();
IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {

                    builder.AddSqlServer(
                        "Data Source=192.168.88.113\\sql2017;Initial Catalog=__learning;User ID=sa;Password=123456;Application Name=MoghimAgencyServices.net8",
                        "Sample Service",
                        "1.0.0.102");
                })
                .ConfigureServices((ctx, services) =>
                {
                    var connectionstirngBuilder = new SqlConnectionStringBuilder()
                    {
                        DataSource = "192.168.88.113\\sql2017",
                        InitialCatalog = "learning",
                        UserID = "sa",
                        Password = "123456",
                        ApplicationName = "SRH",
                        MultipleActiveResultSets = true,
                        TrustServerCertificate = true
                    };

                    services.AddDbContext<SampleDbContext>(opts => opts.UseSqlServer(connectionstirngBuilder.ConnectionString));

                    services.Configure<EmailServiceOptions>(ctx.Configuration.GetSection("EmailService"));
                    services.AddHostedService<ServiceWorker>();
                })
                ;