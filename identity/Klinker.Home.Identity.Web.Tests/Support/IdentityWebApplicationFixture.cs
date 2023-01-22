using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Klinker.Home.Identity.Web.Tests.Support;

public class IdentityWebApplicationFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var identityDatabase = $"{Guid.NewGuid()}";
        builder.ConfigureServices(services =>
        {
            services.Remove<DbContextOptions<PersistedGrantDbContext>>()
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