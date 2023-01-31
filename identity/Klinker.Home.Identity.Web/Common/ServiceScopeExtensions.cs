using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common;

public static class ServiceScopeExtensions
{
    public static async Task MigrateDatabaseAsync<T>(this IServiceScope scope)
        where T : DbContext
    {
        await using var context = scope.ServiceProvider.GetRequiredService<T>();
        await context.Database.MigrateAsync();
    }
}
