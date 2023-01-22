using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Klinker.Home.Identity.Web.Tests.Support;

public class IdentityWebApplication : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var identityDatabase = $"{Guid.NewGuid()}";
        builder.ConfigureAppConfiguration(config =>
        {
            config.AddInMemoryCollection(
                new[]
                {
                    new KeyValuePair<string, string?>(
                        "Identity:Admin:Username",
                        TestAdminUser.Default.Username
                    ),
                    new KeyValuePair<string, string?>(
                        "Identity:Admin:Password",
                        TestAdminUser.Default.Password
                    )
                }
            );
        });
        builder.ConfigureServices(services =>
        {
            services
                .Remove<DbContextOptions<PersistedGrantDbContext>>()
                .Remove<DbContextOptions<ConfigurationDbContext>>();

            services.AddDbContext<PersistedGrantDbContext>(opts =>
            {
                opts.UseInMemoryDatabase(identityDatabase);
            });
            services.AddDbContext<ConfigurationDbContext>(opts =>
            {
                opts.UseInMemoryDatabase(identityDatabase);
            });
        });
    }
}
