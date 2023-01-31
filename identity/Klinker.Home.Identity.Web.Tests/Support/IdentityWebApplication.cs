using Duende.IdentityServer.EntityFramework.DbContexts;
using Klinker.Home.Identity.Web.Common.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Klinker.Home.Identity.Web.Tests.Support;

public class IdentityWebApplication : WebApplicationFactory<Program>
{
    private static int _port = 8000;
    private IHost? _host;

    public string BaseAddress
    {
        get
        {
            EnsureServerReady();
            return ClientOptions.BaseAddress.ToString();
        }
    }

    public T GetService<T>()
        where T : notnull
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var identityDatabase = $"{Guid.NewGuid()}";
        builder.ConfigureServices(services =>
        {
            services
                .Remove<DbContextOptions<PersistedGrantDbContext>>()
                .Remove<DbContextOptions<ConfigurationDbContext>>()
                .Remove<DbContextOptions<KlinkerIdentityDbContext>>();

            services.AddDbContext<KlinkerIdentityDbContext>(opts =>
            {
                opts.UseInMemoryDatabase(identityDatabase);
            });
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

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var testHost = builder.Build();
        builder.ConfigureWebHost(b => b.UseUrls($"http://localhost:{_port++}").UseKestrel());
        _host = builder.Build();
        _host.Start();

        var server = _host.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>();
        ClientOptions.BaseAddress = addresses!.Addresses.Select(x => new Uri(x)).Last();
        testHost.Start();
        return testHost;
    }

    private void EnsureServerReady()
    {
        if (_host is null)
        {
            using var _ = CreateDefaultClient();
        }
    }

    public override ValueTask DisposeAsync()
    {
        _host?.Dispose();
        return base.DisposeAsync();
    }
}
