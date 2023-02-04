using Duende.IdentityServer.EntityFramework.DbContexts;
using Klinker.Home.Identity.Web.Common.Storage;
using Serilog;

namespace Klinker.Home.Identity.Web.Common;

public static class WebApplicationExtensions
{
    public static WebApplication UseKlinkerHomeIdentityWeb(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseIdentityServer();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapRazorPages();
        return app;
    }

    public static async Task RunMigrationsAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        await scope.MigrateDatabaseAsync<ConfigurationDbContext>();
        await scope.MigrateDatabaseAsync<PersistedGrantDbContext>();
        await scope.MigrateDatabaseAsync<KlinkerIdentityDbContext>();
    }
}
