
using IS.Order.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace IS.Order.API.IntegrationTest.Base;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Configure Serilog
        builder.UseSerilog((context, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext());

        builder.ConfigureServices(services =>
        {
            
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connection);
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                try
                {
                    TestDataInitializer.InitializeDbForTests(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            };
        });
    }

    public HttpClient GetAnonymousClient()
    {
        return CreateClient();
    }
}