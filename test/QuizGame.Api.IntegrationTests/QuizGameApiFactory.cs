using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Infrastructure.Contexts;
using Testcontainers.MsSql;

namespace QuizGame.Api.IntegrationTests;

public class QuizGameApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPortBinding(1433, 1433)
        .Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.Remove(services.Single(s => s.ServiceType == typeof(DbContextOptions<QuizGameDataContext>)));
            services.AddDbContext<QuizGameDataContext>(options =>
            {
                var connectionBuilder = new SqlConnectionStringBuilder(_container.GetConnectionString())
                {
                    InitialCatalog = "QuizGame"
                };
                options.UseSqlServer(connectionBuilder.ToString());
            });
        });

        builder.UseEnvironment("IntegrationTesting");
    }

    public new async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}
