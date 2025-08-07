using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ParsMedeq.Application.Options;
using ParsMedeq.Server;
using SRH.ServiceInstaller;
using SRH.Sql.ConfigProvider;
using SRH.Sql.ConfigProvider.Persistance.ValueObjects;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

var processEnv = CommandArgReader.Read(
    builder.Configuration,
    "env",
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development");
Console.WriteLine($"Process Env = {processEnv}");


builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.Limits.MaxConcurrentConnections = 10000;  // Set the number of concurrent connections
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MBs
    options.Limits.MaxResponseBufferSize = 10 * 1024 * 1024;  // 10 MBs
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2); // Timeout for keeping the connection alive
    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30); // Timeout for request headers
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var authSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<AuthenticationOptions>>().Value;

    setup.AddSecurityDefinition("userId", new OpenApiSecurityScheme()
    {
        Name = "userid",
        In = ParameterLocation.Query,
        Type = SecuritySchemeType.ApiKey,
        Scheme = string.Empty,
    });
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "userId"
                },
                Scheme = "",
                Name = "userid",
                In = ParameterLocation.Query,

            },
            []
        }
    });

    setup.AddSecurityDefinition("profileId", new OpenApiSecurityScheme() { Name = "profileid", In = ParameterLocation.Query, Type = SecuritySchemeType.ApiKey });
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "profileId"
                },
                Scheme = "",
                Name = "profileId",
                In = ParameterLocation.Query,

            },
            []
        }
    });

    setup.AddSecurityDefinition(authSettings.CSRFTokenHeaderName, new OpenApiSecurityScheme()
    {
        Name = "X-CSRF-TOKEN",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = string.Empty,
    });
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-CSRF-TOKEN"
                },
                Scheme = "",
                Name = "X-CSRF-TOKEN",
                In = ParameterLocation.Header,

            },
            []
        }
    });

});
builder.Services.AddOutputCache();
builder.Services.AddAntiforgery(opts =>
{
    var authSettings = builder.Services.BuildServiceProvider()!.GetRequiredService<IOptions<AuthenticationOptions>>().Value;

    opts.HeaderName = authSettings.CSRFTokenHeaderName;
    opts.Cookie.Name = authSettings.CSRFTokenCookieName;
    opts.Cookie.SecurePolicy = authSettings.AntiForgeyCookieSecurePolicy;
    opts.Cookie.SameSite = authSettings.CookieOptions.SameSite;
});
builder.Services
    .AddFluentEmail("youremail@gmail.com")
    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential("roozbeh.hoseiny@gmail.com", "coyqmsoztqlwjgck"),
        EnableSsl = true,
    });



builder.Configuration.AddSRHDbConfig(
    builder.Configuration.GetSection("Database:DpiConfig").Get<SqlConnectionStringBuilder>()!.ConnectionString,
    processEnv,
    new SettingApplicationName("Dpi_Tsp"),
    SettingVersion.Version1,
    TimeSpan.FromSeconds(5),
    true);

ServiceInstallerHelper.InstallServicesRecursively(builder.Services,
           builder.Configuration,
           ApiAssemblyReference.Assembly);
////////////////////////////////
var app = builder.Build();
app.UseHsts();
app.UseDefaultFiles();
app.MapStaticAssets();
app.UseAntiforgery();

app.UseCors(opts =>
{
    opts.AllowAnyMethod();
    opts.AllowAnyHeader();
    opts.AllowAnyOrigin();
});


app.UseOutputCache();
app.MapMinimalEndpoits();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Collapse all endpoints by default
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});
app.UseAuthorization();
app.UseExceptionHandler();

app.MapControllers();
app.MapHealthChecks("/health");
app.MapFallbackToFile("/index.html");
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
static class CommandArgReader
{
    public static string Read(IConfiguration config, string key, string defaultValue)
    {
        var result = config[key];
        return string.IsNullOrWhiteSpace(result) ? defaultValue : result;
    }
}